# Multiples Cropper
Simple CLI cropper for images that makes their size divisible by/a multiple of a number of your choice. Useful for preparing datasets for some machine learning algorithms like BasicSR's training for example.

## Requirements
I think you need **.NET Framework 4.6 Runtime** to run this, as that is what it's made with, but I'm not 100% sure.

## Usage

**Crop all images in current folder to be divisible by 4 (default):**

```MultiplesCropper *.png```

**Crop all images in current folder to be divisible by 2:**

```MultiplesCropper -m 2 *.png```

or

```MultiplesCropper --multiple 2 *.png```

**Crop specific images in current folder to be divisible by 8:**

```MultiplesCropper -m 8 file1.png file2.png file3.png```

or

```MultiplesCropper file1.png file2.png file3.png -m 8```


**Set suffix for cropped images (default is _multiplescrop):**

```MultiplesCropper -s _myownsuffix *.png```

or

```MultiplesCropper -suffix _myownsuffix *.png```

**The order of the arguments doesn't matter. You can pass arguments and filenames in any order you want, like here:**
```MultiplesCropper --multiple 8 *.png --s _blahblah test1.jpg test2.jpg```

## Algorithm
This tool will simply take away as many pixels on the right side and bottom of the image as necessary to make it divisible by the number you specify. There is currently no option to set or change the cropping alignment, though it should be easy to implement.

## Important note regarding lossy image formats 
Technically this supports JPEG files and other lossy formats supported by C#'s System.Drawing.Image stuff. This tool will simply save each file back to the original format, however this process will almost guaranteedly introduce quality loss and there are currently no options to control things like JPEG compression quality. 

There is no lossless JPEG crop option available.