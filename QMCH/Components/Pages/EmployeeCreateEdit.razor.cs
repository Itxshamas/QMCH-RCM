using QMCH.Models;
using QMCH.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace QMCH.Components.Pages
{
    public partial class EmployeeCreateEdit
    {
        [SupplyParameterFromQuery]
        public int? Id { get; set; }

        [Inject]
        private IEmployeeService EmployeeServiceInstance { get; set; } = null!;

        [Inject]
        private NavigationManager NavigationInstance { get; set; } = null!;

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;

        private Employee model = new();
        private bool isEdit = false;
        private string activeSection = "personal";
        private string? photoPreview;
        private bool saveAndClose = false;
        private string photoInputId = $"photo-input-{Guid.NewGuid()}";
        private bool showNavigation = true;

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue && Id > 0)
            {
                var employee = await EmployeeServiceInstance.GetByIdAsync(Id.Value);
                if (employee != null)
                {
                    model = employee;
                    isEdit = true;
                    LoadPhotoPreview();
                }
                else
                {
                    NavigateBack();
                }
            }
        }

        private void SetActiveSection(string section)
        {
            activeSection = section;
        }

        private void ToggleNavigation()
        {
            showNavigation = !showNavigation;
        }

        private void SetSaveAndClose()
        {
            saveAndClose = true;
        }

        private async Task Save(EditContext context)
        {
            try
            {
                if (isEdit)
                {
                    await EmployeeServiceInstance.UpdateAsync(model);
                }
                else
                {
                    await EmployeeServiceInstance.CreateAsync(model);
                }

                if (saveAndClose)
                {
                    NavigateBack();
                }
                else
                {
                    // Auto-navigate to next section
                    string nextSection = GetNextSection(activeSection);
                    if (nextSection != activeSection)
                    {
                        SetActiveSection(nextSection);
                    }
                    saveAndClose = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving employee: {ex.Message}");
            }
        }

        private string GetNextSection(string currentSection)
        {
            return currentSection switch
            {
                "personal" => "contract",
                "contract" => "legal",
                "legal" => "contacts",
                "contacts" => "login",
                _ => "personal"
            };
        }

        private async Task TriggerPhotoUpload()
        {
            await JSRuntime.InvokeVoidAsync("triggerFileInput", photoInputId);
        }

        private async Task OnPhotoSelected(InputFileChangeEventArgs e)
        {
            try
            {
                var file = e.File;

                // Validate file type
                var allowedMimeTypes = new[] { "image/png", "image/jpeg" };
                if (!allowedMimeTypes.Contains(file.ContentType))
                {
                    return; // File type not allowed
                }

                // Validate file size (max 5MB)
                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                if (file.Size > maxFileSize)
                {
                    return; // File too large
                }

                // Read file data
                using (var stream = file.OpenReadStream(maxAllowedSize: maxFileSize))
                {
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    model.PhotoData = memoryStream.ToArray();
                    model.PhotoMimeType = file.ContentType;
                }

                // Generate preview
                LoadPhotoPreview();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading photo: {ex.Message}");
            }
        }

        private void LoadPhotoPreview()
        {
            if (model.PhotoData != null && model.PhotoData.Length > 0)
            {
                var base64String = Convert.ToBase64String(model.PhotoData);
                var mimeType = model.PhotoMimeType ?? "image/jpeg";
                photoPreview = $"data:{mimeType};base64,{base64String}";
            }
            else
            {
                photoPreview = null;
            }
        }

        private void DeletePhoto()
        {
            model.PhotoData = null;
            model.PhotoMimeType = null;
            photoPreview = null;
        }

        private void NavigateBack()
        {
            NavigationInstance.NavigateTo("/caregiver/list");
        }
    }
}
