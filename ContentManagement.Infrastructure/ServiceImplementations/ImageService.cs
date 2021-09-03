using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContentManagement.Application.Contracts;
using ContentManagement.Application.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.Infrastructure.ServiceImplementations
{
    public class ImageService : IIMageService
    {
        private readonly AccountSettings _accountSettings;
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        public ImageService(IOptions<AccountSettings> options, IConfiguration config)
        {
            _configuration = config;
            _accountSettings = options.Value;
            _cloudinary = new Cloudinary(new Account(_accountSettings.CloudName, _accountSettings.ApiSecret, _accountSettings.ApiKey));
        }



        public async Task<UploadResult> UploadImage(IFormFile model)
        {
            bool pictureFormat = false;

            List<string> listOfPhotoExtensions =
                                            _configuration
                                            .GetSection("PhotoSettings:Extensions")
                                            .Get<List<string>>();

            int pictureSize = _configuration.GetSection("PhotoSettings:Size").Get<int>();

            for (int i = 0; i < listOfPhotoExtensions.Count; i++)
            {
                var ele = model.FileName.ToLower();
                if (ele.EndsWith(listOfPhotoExtensions[i]))
                {
                    pictureFormat = true;
                    break;
                }
            }

            if (!pictureFormat || model == null) throw new Exception("picture format not supported");

            if (model.Length > pictureSize) throw new Exception("photo is greater than 2mb");

            ImageUploadResult uploadResult;

            //fetch image as a stream of data

            using var imageStream = model.OpenReadStream();
            {
                var fileName = Guid.NewGuid().ToString() + "_" + model.Name;

                ImageUploadParams imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, imageStream),

                    Transformation = new Transformation().Crop("thumb")
                                                        .Gravity("face")
                                                        .Width(1000)
                                                        .Height(1000)
                                                        .Radius(40)
                };

                uploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            }

            return uploadResult;

        }

        public async Task<DelResResult> DeleteImage(string publicId)
        {
            DelResParams deleteParams = new DelResParams
            {
                PublicIds = new List<string> { publicId },
                All = true,
                Invalidate = true,
                KeepOriginal = false
            };


            var result = await _cloudinary.DeleteResourcesAsync(deleteParams);

            return result;
        }
    }
}
