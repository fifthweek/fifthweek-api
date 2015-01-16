namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.IO;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    public class ImageService : IImageService
    {
        public void Resize(Stream input, Stream output, int width, int height, ResizeBehaviour resizeBehaviour)
        {
            switch (resizeBehaviour)
            {
                case ResizeBehaviour.MaintainAspectRatio:
                    this.ResizeMaintainAspectRatio(input, output, width, height);
                    break;

                case ResizeBehaviour.CropToAspectRatio:
                    this.ResizeCropToAspectRatio(input, output, width, height);
                    break;
            }
        }

        public void ResizeMaintainAspectRatio(Stream input, Stream output, int width, int height)
        {
            using (var image = new MagickImage(input))
            {
                if (image.Width > width || image.Height > height)
                {
                    image.Resize(width, height);
                }

                image.Write(output);
            }
        }

        public void ResizeCropToAspectRatio(Stream input, Stream output, int width, int height)
        {
            using (var image = new MagickImage(input))
            {
                if (image.Width > width && image.Height > height)
                {
                    image.Resize(new MagickGeometry(width, height) { FillArea = true });
                }

                var desiredAspectRatio = width / (double)height;
                var currentAspectRatio = image.Width / (double)image.Height;

                if (desiredAspectRatio != currentAspectRatio)
                {
                    if (desiredAspectRatio > currentAspectRatio)
                    {
                        image.Crop(width, (int)Math.Round(height * currentAspectRatio / desiredAspectRatio));
                    }
                    else
                    {
                        image.Crop((int)Math.Round(width * desiredAspectRatio / currentAspectRatio), height);
                    }
                }

                image.Write(output);
            }
        }
    }
}