using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class FileService: IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public FileService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        //metadata method
        //a method to retrieve metadata associated with a file in blob storage.
        //This method return metadata properties such as file size, content type, creation date, etc.
        public async Task<IDictionary<string, string>> GetMetadata(string name)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(name));
            }

            try
            {
                // Retrieve blob container
                var containerInstance = _blobServiceClient.GetBlobContainerClient("patientrecords");

                // Check if blob exists
                if (!await containerInstance.ExistsAsync())
                {
                    throw new InvalidOperationException("Blob container does not exist.");
                }

                // Retrieve blob client
                var blobInstance = containerInstance.GetBlobClient(name);

                // Check if blob exists
                if (!await blobInstance.ExistsAsync())
                {
                    throw new FileNotFoundException($"Blob '{name}' not found.");
                }

                // Retrieve blob properties
                BlobProperties blobProperties = await blobInstance.GetPropertiesAsync();

                // Return metadata
                return blobProperties.Metadata;
            }
            catch (ArgumentException)
            {
                // Re-throw ArgumentException
                throw;
            }
            catch (InvalidOperationException)
            {
                // Re-throw InvalidOperationException
                throw;
            }
            catch (FileNotFoundException)
            {
                // Re-throw FileNotFoundException
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving metadata.", ex);
            }
        }

        //update method
        //a method to update an existing file in blob storage.
        //This method takes the name of the file to update and the new content to upload.
        public async Task Update(string name, Stream newContent)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(name));
            }

            if (newContent == null || newContent.Length == 0)
            {
                throw new ArgumentException("New content stream cannot be null or empty.", nameof(newContent));
            }

            try
            {
                // Retrieve blob container
                var containerInstance = _blobServiceClient.GetBlobContainerClient("patientrecords");

                // Check if blob container exists
                if (!await containerInstance.ExistsAsync())
                {
                    throw new InvalidOperationException("Blob container does not exist.");
                }

                // Retrieve blob client
                var blobInstance = containerInstance.GetBlobClient(name);

                // Upload new content
                await blobInstance.UploadAsync(newContent);
            }
            catch (ArgumentException)
            {
                // Re-throw ArgumentException
                throw;
            }
            catch (InvalidOperationException)
            {
                // Re-throw InvalidOperationException
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating the blob.", ex);
            }
        }

        //download to file method
        //a method to download a file from blob storage to a local file on the server.
        //This method takes the name of the file to download and the path where it should be saved.
        public async Task DownloadToFile(string name, string filePath)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Blob name cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            try
            {
                // Retrieve blob container
                var containerInstance = _blobServiceClient.GetBlobContainerClient("patientrecords");

                // Check if blob container exists
                if (!await containerInstance.ExistsAsync())
                {
                    throw new InvalidOperationException("Blob container does not exist.");
                }

                // Retrieve blob client
                var blobInstance = containerInstance.GetBlobClient(name);

                // Download blob to file
                await blobInstance.DownloadToAsync(filePath);
            }
            catch (ArgumentException)
            {
                // Re-throw ArgumentException
                throw;
            }
            catch (InvalidOperationException)
            {
                // Re-throw InvalidOperationException
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while downloading the blob to file.", ex);
            }
        }

        //list method
        // a method to list all files in the blob storage container.
        // This method return a list of file names or file metadata.
        public async Task<IEnumerable<string>> List()
        {
            // Initialize an empty list to store blob names
            var fileList = new List<string>();

            try
            {
                // Retrieve blob container
                var containerInstance = _blobServiceClient.GetBlobContainerClient("patientrecords");

                // Check if blob container exists
                if (!await containerInstance.ExistsAsync())
                {
                    throw new InvalidOperationException("Blob container does not exist.");
                }

                // Retrieve list of blobs
                var blobs = containerInstance.GetBlobs();

                // Iterate through blobs and add their names to the list
                foreach (var blobItem in blobs)
                {
                    fileList.Add(blobItem.Name);
                }

                return fileList;
            }
            catch (InvalidOperationException)
            {
                // Re-throw InvalidOperationException
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while listing blobs.", ex);
            }
        }

        //delete method
        //a method to delete files from the blob storage.
        //This method takes the name of the file to delete as a parameter.
        public async Task Delete(string name)
        {
            try
            {
                // Retrieve blob container
                var containerInstance = _blobServiceClient.GetBlobContainerClient("patientrecords");

                // Retrieve blob client
                var blobInstance = containerInstance.GetBlobClient(name);

                // Check if blob exists
                if (!await blobInstance.ExistsAsync())
                {
                    throw new InvalidOperationException("Blob does not exist.");
                }

                // Delete blob if it exists
                await blobInstance.DeleteIfExistsAsync();
            }
            catch (InvalidOperationException)
            {
                // Re-throw InvalidOperationException
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while deleting the blob.", ex);
            }
        }

        //get method
        //a method to get files from the blob storage.
        //This method takes the name of the file to retrieve as a parameter.
        public async Task<Stream> Get(string name)
        {
            try
            {
                // Retrieve blob container
                var containerInstance = _blobServiceClient.GetBlobContainerClient("patientrecords");

                // Retrieve blob client
                var blobInstance = containerInstance.GetBlobClient(name);

                // Check if blob exists
                if (!await blobInstance.ExistsAsync())
                {
                    throw new InvalidOperationException("Blob does not exist.");
                }

                // Download blob content
                var downloadContent = await blobInstance.DownloadAsync();

                // Return the content stream
                return downloadContent.Value.Content;
            }
            catch (InvalidOperationException)
            {
                // Re-throw InvalidOperationException
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while downloading the blob content.", ex);
            }
        }

        //upload method
        //a method to upload files.
        //This method takes the file to upload as a parameter.
        public async Task Upload(FileModel fileModel)
        {
            try
            {
                // Check if the file model is null
                if (fileModel == null)
                {
                    throw new ArgumentNullException(nameof(fileModel), "File model cannot be null.");
                }

                // Check if the patient file is null
                if (fileModel.PatientFile == null)
                {
                    throw new ArgumentNullException(nameof(fileModel.PatientFile), "Patient file cannot be null.");
                }

                // Retrieve blob container
                var containerInstance = _blobServiceClient.GetBlobContainerClient("patientrecords");

                // Retrieve blob client
                var blobInstance = containerInstance.GetBlobClient(fileModel.PatientFile.FileName);

                // Upload file content
                await blobInstance.UploadAsync(fileModel.PatientFile.OpenReadStream());
            }
            catch (ArgumentNullException)
            {
                // Re-throw ArgumentNullException
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while uploading the file.", ex);
            }
        }




    }//end class
}//end namespace
