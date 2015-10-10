namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.IO;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    public class ImageService : IImageService
    {
        public void Resize(MagickImage input, Stream output, int width, int height, ResizeBehaviour resizeBehaviour, ProcessingBehaviour processingBehaviour)
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

            if (processingBehaviour == ProcessingBehaviour.Darken)
            {
                input.Colorize(new MagickColor(0, 0, 0), new Percentage(50));
            }

            input.Write(output);
        }

        private void ResizeMaintainAspectRatio(MagickImage input, Stream output, int width, int height)
        {
            if (input.Width > width || input.Height > height)
            {
                input.Resize(width, height);
            }
        }

        private void ResizeCropToAspectRatio(MagickImage input, Stream output, int width, int height)
        {
            if (input.Width > width && input.Height > height)
            {
                input.Resize(new MagickGeometry(width, height) { FillArea = true });
            }

            var desiredAspectRatio = width / (double)height;
            var currentAspectRatio = input.Width / (double)input.Height;

            if (desiredAspectRatio != currentAspectRatio)
            {
                if (desiredAspectRatio > currentAspectRatio)
                {
                    input.Crop(width, (int)Math.Round(input.Height * currentAspectRatio / desiredAspectRatio));
                }
                else
                {
                    input.Crop((int)Math.Round(input.Width * desiredAspectRatio / currentAspectRatio), height);
                }
            }
        }
    }
}