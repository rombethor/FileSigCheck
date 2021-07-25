
## File Signature Checker

This .NET Standard 2.0 library contains code for checking file signatures in their byte marks.

The file signatures were taken fromt his helpful resource: https://filesignatures.net/ (Thanks to them!)

### Supported Extensions

It currently contains file signatures for the following extensions:
- .png
- .jpg
- .jpeg
- .gif
- .doc
- .docx
- .ppt
- .pptx
- .xls
- .xlsx
- .pdf
- .mp3
- .wav
- .ogg
- .mpg
- .m4a
- .xml

I will add more signatures if they are requested, or if I desire them.

### Base 64 Images

There is a method for checking base 64 strings to see whether they are images.

Use as `FileSigCheck.IsValidBase64Image(base64string)`.

Currently, It will strip the metadata from the start of the string, ignoring the "image/*" MIME declarations.

### Usage

A typical scenario in which I have implented this is with a file upload in .NET Core API:

```
static readonly string[] fileExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };

public async Task<IActionResult> UploadImage([FromForm]IFormFile file)
{
    var ext = Path.GetExtension(file.FileName);

    /* Check the file extension in permissible file types */
    if(!fileExtensions.Contains(ext))
    {
        return StatusCode(StatusCodes.Status415UnsupportedMediaType);
    }
    /* Get the file data */
    using (var rs = file.OpenReadStream())
    {
        /* perform check  */
        if (!FileSecurity.IsValidFileSignature(file.FileName, rs, fileExtensions))
        {
            ModelState.AddModelError("File", $"Is not recognised as a {Path.GetExtension(file.FileName)} file.");
            return BadRequest(ModelState);
        }

        /* Then do what you would normally do with that file... */
    }

    return Ok();
}

```
