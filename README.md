
## File Signature Checker

This .NET Standard 2.0 library contains code for checking file signatures. 

File signatures are tell-tale signs of a file format, usually a part of the file's header in the file contents, which can be used
to determine the format or metadata of the file.

The file signatures were taken from this helpful resource: https://filesignatures.net/ (Thanks to them!) and also from Wikipedia.

### Supported Extensions

It currently contains file signatures for the following extensions:
- .png
- .jpg
- .jpeg
- .tif
- .tiff
- .webp
- .gif
- .doc
- .docx
- .ppt
- .pptx
- .xls
- .xlsx
- .rtf
- .pdf
- .mp3
- .wav
- .avi
- .ogg
- .mpg
- .m4a
- .xml
- .rar
- .exe
- .blend

I will add more signatures if they are requested, or if I desire them.  I will also consider pull requests which add more file
signatures.  If you do add more signatures, please provide a reference to the source of the signature definition within the 
pull request.

### Base 64 Images

There is a method for checking base 64 strings Data URIs to see whether they are valid images or audio clips.

Use as `bool result = FileSignatureUtil.IsDataUriSignatureValid(myDataUri);`

It will use the mime-type at the beginning of the Data URI to determine which signature to check.  So `image.png` will compare to a `.png` file.

Currently, it only works for image/jpeg, image/png, image/gif, image/tiff, audio/ogg an audio/mpeg.  Help is wanted to expand this list.

### Usage

Once you have a file stream accessible, if you want to check against the file name's extension, use:

`bool result = FileSignatureUtil.IsFileSignatureValid("myImage.png", filestream);`

Otherwise you can specify a set of extensions and that the data should match any of them:

```c#
static readonly string[] fileExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };

bool result = FileSignatureUtil.IsFileSignatureValid(filestream, fileExtensions);
```

A typical scenario in which this might be used is with a file upload in .NET Core API:

```c#
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
        if (!FileSignatureUtil.IsFileSignatureValid(rs, fileExtensions))
        {
            ModelState.AddModelError("File", $"Is not recognised as a {ext} file.");
            return BadRequest(ModelState);
        }

        /* Then do what you would normally do with that file... */
    }

    return Ok();
}

```
