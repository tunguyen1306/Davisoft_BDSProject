using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Cyotek.GhostScript
{
  // Cyotek GhostScript API Integration
  // Copyright (c) 2011 Cyotek. All Rights Reserved.
  // http://cyotek.com

  // If you use this library in your applications, attribution or donations are welcome.

  /// <summary>
  ///  Wrapper class for the GhostScript native API
  /// </summary>
  public class GhostScriptAPI : IDisposable
  {
  #region  Private Member Declarations  

    private GCHandle[] _argumentHandles;
    private GCHandle _argumentPointersHandle;
    private IntPtr _ghostScriptInstance;

  #endregion  Private Member Declarations  

  #region  Public Constructors  

    /// <summary>
    ///  Initializes a new instance of the GhostScriptAPI class.
    /// </summary>
    public GhostScriptAPI()
    {
      NativeMethods.gsapi_new_instance(out _ghostScriptInstance, IntPtr.Zero);
    }

  #endregion  Public Constructors  

  #region  Public Class Methods  

    /// <summary>
    ///  Returns the GhostScript command name for the given enumerated command
    /// </summary>
    /// <exception cref="ArgumentException">  Thrown when one or more arguments have unsupported or illegal values. </exception>
    /// <param name="command">  The command. </param>
    /// <returns>
    ///  The name of the specified command required for use with the GhostScript API.
    /// </returns>
    public static string GetCommandName(GhostScriptCommand command)
    {
      string result;

      switch (command)
      {
        case GhostScriptCommand.ColorScreen:
          result = "dCOLORSCREEN";
          break;
        case GhostScriptCommand.DitherPPI:
          result = "dDITHERPPI";
          break;
        case GhostScriptCommand.Interpolate:
          result = "dDOINTERPOLATE";
          break;
        case GhostScriptCommand.NoInterpolate:
          result = "dNOINTERPOLATE";
          break;
        case GhostScriptCommand.TextAlphaBits:
          result = "dTextAlphaBits";
          break;
        case GhostScriptCommand.GraphicsAlphaBits:
          result = "dGraphicsAlphaBits";
          break;
        case GhostScriptCommand.AlignToPixels:
          result = "dAlignToPixels";
          break;
        case GhostScriptCommand.GridToFitTT:
          result = "dGridFitTT";
          break;
        case GhostScriptCommand.UseCIEColor:
          result = "dUseCIEColor";
          break;
        case GhostScriptCommand.NoCIE:
          result = "dNOCIE";
          break;
        case GhostScriptCommand.NoSubstituteDeviceColors:
          result = "dNOSUBSTDEVICECOLORS";
          break;
        case GhostScriptCommand.NoPSICC:
          result = "dNOPSICC";
          break;
        case GhostScriptCommand.NoTransparency:
          result = "dNOTRANSPARENCY";
          break;
        case GhostScriptCommand.NoTN5044:
          result = "dNO_TN5044";
          break;
        case GhostScriptCommand.DoPS:
          result = "dDOPS";
          break;
        case GhostScriptCommand.FixedMedia:
          result = "dFIXEDMEDIA";
          break;
        case GhostScriptCommand.FixedResolution:
          result = "dFIXEDRESOLUTION";
          break;
        case GhostScriptCommand.Orient1:
          result = "dORIENT1";
          break;
        case GhostScriptCommand.DeviceWidthPoints:
          result = "dDEVICEWIDTHPOINTS";
          break;
        case GhostScriptCommand.DeviceHeightPoint:
          result = "dDEVICEHEIGHTPOINTS";
          break;
        case GhostScriptCommand.DefaultPaperSize:
          result = "sDEFAULTPAPERSIZE";
          break;
        case GhostScriptCommand.DiskFonts:
          result = "dDISKFONTS";
          break;
        case GhostScriptCommand.LocalFonts:
          result = "dLOCALFONTS";
          break;
        case GhostScriptCommand.NoCCFonts:
          result = "dNOCCFONTS";
          break;
        case GhostScriptCommand.NoFontMap:
          result = "dNOFONTMAP";
          break;
        case GhostScriptCommand.NoFontPath:
          result = "dNOFONTPATH";
          break;
        case GhostScriptCommand.NoPlatformFonts:
          result = "dNOPLATFONTS";
          break;
        case GhostScriptCommand.NoNativeFontMap:
          result = "dNONATIVEFONTMAP";
          break;
        case GhostScriptCommand.FontMap:
          result = "sFONTMAP";
          break;
        case GhostScriptCommand.FontPath:
          result = "sFONTPATH";
          break;
        case GhostScriptCommand.SubstituteFont:
          result = "sSUBSTFONT";
          break;
        case GhostScriptCommand.OldCFFParser:
          result = "dOLDCFF";
          break;
        case GhostScriptCommand.GenericResourceDirectory:
          result = "sGenericResourceDir";
          break;
        case GhostScriptCommand.FontResourceDirectory:
          result = "sFontResourceDir";
          break;
        case GhostScriptCommand.Batch:
          result = "dBATCH";
          break;
        case GhostScriptCommand.NoPagePrompt:
          result = "dNOPAGEPROMPT";
          break;
        case GhostScriptCommand.NoPause:
          result = "dNOPAUSE";
          break;
        case GhostScriptCommand.NoPrompt:
          result = "dNOPROMPT";
          break;
        case GhostScriptCommand.Silent:
          result = "q";
          break;
        case GhostScriptCommand.Quiet:
          result = "dQUIET";
          break;
        case GhostScriptCommand.ShortErrors:
          result = "dSHORTERRORS";
          break;
        case GhostScriptCommand.StandardOut:
          result = "sstdout";
          break;
        case GhostScriptCommand.TTYPause:
          result = "dTTYPAUSE";
          break;
        case GhostScriptCommand.NoDisplay:
          result = "dNODISPLAY";
          break;
        case GhostScriptCommand.Device:
          result = "sDEVICE";
          break;
        case GhostScriptCommand.OutputFile:
          result = "sOutputFile";
          break;
        case GhostScriptCommand.IgnoreMultipleCopies:
          result = "d.IgnoreNumCopies";
          break;
        case GhostScriptCommand.EPSCrop:
          result = "dEPSCrop";
          break;
        case GhostScriptCommand.EPPSFitPage:
          result = "dEPSFitPage";
          break;
        case GhostScriptCommand.NoEPS:
          result = "dNOEPS";
          break;
        case GhostScriptCommand.DefaultGrayProfile:
          result = "sDefaultGrayProfile";
          break;
        case GhostScriptCommand.DefaultRGBProfile:
          result = "sDefaultRGBProfile";
          break;
        case GhostScriptCommand.DefaultCMYKProfile:
          result = "sDefaultCMYKProfile";
          break;
        case GhostScriptCommand.DeviceNProfile:
          result = "sDeviceNProfile";
          break;
        case GhostScriptCommand.ProofProfile:
          result = "sProofProfile";
          break;
        case GhostScriptCommand.DeviceLinkProfile:
          result = "sDeviceLinkProfile";
          break;
        case GhostScriptCommand.NamedProfile:
          result = "sNamedProfile";
          break;
        case GhostScriptCommand.OutputICCProfile:
          result = "sOutputICCProfile";
          break;
        case GhostScriptCommand.RenderIntent:
          result = "sRenderIntent";
          break;
        case GhostScriptCommand.GraphicICCProfile:
          result = "sGraphicICCProfile";
          break;
        case GhostScriptCommand.GraphicIntent:
          result = "sGraphicIntent";
          break;
        case GhostScriptCommand.ImageICCProfile:
          result = "sImageICCProfile";
          break;
        case GhostScriptCommand.ImageIntent:
          result = "sImageIntent";
          break;
        case GhostScriptCommand.TextICCProfile:
          result = "sTextICCProfile";
          break;
        case GhostScriptCommand.TextIntent:
          result = "sTextIntent";
          break;
        case GhostScriptCommand.OverrideICC:
          result = "dOverrideICC";
          break;
        case GhostScriptCommand.OverrideRI:
          result = "dOverrideRI";
          break;
        case GhostScriptCommand.SourceObjectICC:
          result = "sSourceObjectICC";
          break;
        case GhostScriptCommand.DeviceGrayToK:
          result = "dDeviceGrayToK";
          break;
        case GhostScriptCommand.ICCProfilesDirectory:
          result = "sICCProfilesDir";
          break;
        case GhostScriptCommand.DelayBind:
          result = "dDELAYBIND";
          break;
        case GhostScriptCommand.PdfMarks:
          result = "dDOPDFMARKS";
          break;
        case GhostScriptCommand.JobServer:
          result = "dJOBSERVER";
          break;
        case GhostScriptCommand.NoBind:
          result = "dNOBIND";
          break;
        case GhostScriptCommand.NoCache:
          result = "dNOCACHE";
          break;
        case GhostScriptCommand.NoGC:
          result = "dNOGC";
          break;
        case GhostScriptCommand.NoOuterSave:
          result = "dNOOUTERSAVE";
          break;
        case GhostScriptCommand.NoSafer:
          result = "dNOSAFER";
          break;
        case GhostScriptCommand.Safer:
          result = "dSAFER";
          break;
        case GhostScriptCommand.Strict:
          result = "dSTRICT";
          break;
        case GhostScriptCommand.SystemDictionaryWritable:
          result = "dWRITESYSTEMDICT";
          break;
        case GhostScriptCommand.FirstPage:
          result = "dFirstPage";
          break;
        case GhostScriptCommand.LastPage:
          result = "dLastPage";
          break;
        case GhostScriptCommand.PDFFitPage:
          result = "dPDFFitPage";
          break;
        case GhostScriptCommand.Printed:
          result = "dPrinted";
          break;
        case GhostScriptCommand.UseCropBox:
          result = "dUseCropBox";
          break;
        case GhostScriptCommand.UseTrimBox:
          result = "dUseTrimBox";
          break;
        case GhostScriptCommand.PDFPassword:
          result = "sPDFPassword";
          break;
        case GhostScriptCommand.ShowAnnotations:
          result = "dShowAnnots";
          break;
        case GhostScriptCommand.ShowAcroForm:
          result = "dShowAcroForm";
          break;
        case GhostScriptCommand.NoUserInit:
          result = "dNoUserUnit";
          break;
        case GhostScriptCommand.RenderTTNotDef:
          result = "dRENDERTTNOTDEF";
          break;
        case GhostScriptCommand.Resolution:
          result = "r";
          break;
        case GhostScriptCommand.PaperSize:
          result = "sPAPERSIZE";
          break;
        case GhostScriptCommand.InputFile:
          result = string.Empty;
          break;
        default:
          throw new ArgumentException("Invalid command", "command");
      }

      if (!string.IsNullOrEmpty(result))
        result = "-" + result;

      return result;
    }

    /// <summary>
    ///  Gets a device name for the given image format
    /// </summary>
    /// <exception cref="ArgumentException">  Thrown when one or more arguments have unsupported or illegal values. </exception>
    /// <param name="format"> Describes the image format to use. </param>
    /// <returns>
    ///  The device name which corrosponds to the given image format.
    /// </returns>
    public static string GetDeviceName(ImageFormat format)
    {
      string result;

      switch (format)
      {
        case ImageFormat.Bitmap24:
          result = "bmp16m";
          break;
        case ImageFormat.Bitmap8:
          result = "bmp256";
          break;
        case ImageFormat.BitmapMono:
          result = "bmpgray";
          break;
        case ImageFormat.Jpeg:
          result = "jpeg";
          break;
        case ImageFormat.Png8:
          result = "png256";
          break;
        case ImageFormat.Png24:
          result = "png16m";
          break;
        case ImageFormat.PngMono:
          result = "pnggray";
          break;
        case ImageFormat.Tiff24:
          result = "tiff24nc";
          break;
        case ImageFormat.TiffMono:
          result = "tiffgray";
          break;
        default:
          throw new ArgumentException("Invalid format", "format");
      }

      return result;
    }

    /// <summary>
    ///  Returns the given command and value as a formatted switch for use with the GhostScript API
    /// </summary>
    /// <param name="command">  The command. </param>
    /// <param name="value">    The value. </param>
    /// <returns>
    ///  The format switch.
    /// </returns>
    public static string GetFormattedArgument(GhostScriptCommand command, object value)
    {
      string commandName;
      string operatorChar;
      string commandValue;

      // reformat some values depending on their type
      if (value != null)
      {
        if (value is Enum)
          value = (int)value;
        else if (value is bool)
          value = (bool)value ? "true" : "false";
      }

      // and work out how the names/values will be combined
      commandName = GhostScriptAPI.GetCommandName(command);
      operatorChar = value == null || string.IsNullOrEmpty(commandName) || command == GhostScriptCommand.Resolution ? string.Empty : "=";
      commandValue = value == null ? string.Empty : value.ToString();

      return commandName + operatorChar + commandValue;
    }

    /// <summary>
    ///  Returns the given argument dictionary as an array of formatted switches for use with the GhostScript API
    /// </summary>
    /// <param name="args"> The arguments. </param>
    /// <returns>
    ///  The formatted arguments.
    /// </returns>
    public static string[] GetFormattedArguments(IDictionary<GhostScriptCommand, object> args)
    {
      List<string> translatedArgs;

      translatedArgs = new List<string>();
      foreach (KeyValuePair<GhostScriptCommand, object> pair in args)
        translatedArgs.Add(GhostScriptAPI.GetFormattedArgument(pair.Key, pair.Value));

      return translatedArgs.ToArray();
    }

    public static string GetPaperSizeName(PaperSize paperSize)
    {
      string result;

      switch (paperSize)
      {
        case PaperSize.LedgerPortrait:
          result = "11x17";
          break;
        case PaperSize.Ledger:
          result = "ledger";
          break;
        case PaperSize.Legal:
          result = "legal";
          break;
        case PaperSize.Letter:
          result = "letter";
          break;
        case PaperSize.LetterSmall:
          result = "lettersmall";
          break;
        case PaperSize.ArchE:
          result = "archE";
          break;
        case PaperSize.ArchD:
          result = "archD";
          break;
        case PaperSize.ArchC:
          result = "archC";
          break;
        case PaperSize.ArchB:
          result = "archB";
          break;
        case PaperSize.ArchA:
          result = "archA";
          break;
        case PaperSize.A0:
          result = "a0";
          break;
        case PaperSize.A1:
          result = "a1";
          break;
        case PaperSize.A2:
          result = "a2";
          break;
        case PaperSize.A3:
          result = "a3";
          break;
        case PaperSize.A4:
          result = "a4";
          break;
        case PaperSize.A5:
          result = "a5";
          break;
        case PaperSize.A6:
          result = "a6";
          break;
        case PaperSize.A7:
          result = "a7";
          break;
        case PaperSize.A8:
          result = "a8";
          break;
        case PaperSize.A9:
          result = "a9";
          break;
        case PaperSize.A10:
          result = "a10";
          break;
        case PaperSize.B0:
          result = "isob0";
          break;
        case PaperSize.B1:
          result = "isob1";
          break;
        case PaperSize.B2:
          result = "isob2";
          break;
        case PaperSize.B3:
          result = "isob3";
          break;
        case PaperSize.B4:
          result = "isob4";
          break;
        case PaperSize.B5:
          result = "isob5";
          break;
        case PaperSize.B6:
          result = "isob6";
          break;
        case PaperSize.C0:
          result = "c0";
          break;
        case PaperSize.C1:
          result = "c1";
          break;
        case PaperSize.C2:
          result = "c2";
          break;
        case PaperSize.C3:
          result = "c3";
          break;
        case PaperSize.C4:
          result = "c4";
          break;
        case PaperSize.C5:
          result = "c5";
          break;
        case PaperSize.C6:
          result = "c6";
          break;
        case PaperSize.Foolscap:
          result = "fse";
          break;
        case PaperSize.HalfLetter:
          result = "halfletter";
          break;
        case PaperSize.Hagaki:
          result = "hagaki";
          break;
        default:
          throw new ArgumentException("Invalid paper size", "paperSize");
      }

      return result;
    }

  #endregion  Public Class Methods  

  #region  Public Methods  

    /// <summary>
    ///  Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public virtual void Dispose()
    {
      try
      {
        this.FreeHandles();
        NativeMethods.gsapi_exit(_ghostScriptInstance);
        NativeMethods.gsapi_delete_instance(_ghostScriptInstance);
      }
      catch
      {
        // don't care about errors
      }
    }

    /// <summary>
    ///  Calls the gsapi_init_with_args GhostScript API with the given arguments.
    /// </summary>
    /// <exception cref="GhostScriptException"> Thrown when ghostscript. </exception>
    /// <param name="args"> The arguments. </param>
    /// <returns>
    ///  The GhostScript result code
    /// </returns>
    public int Execute(IDictionary<GhostScriptCommand, object> args)
    {
      return this.Execute(GhostScriptAPI.GetFormattedArguments(args));
    }

    /// <summary>
    ///  Calls the gsapi_init_with_args GhostScript API with the given arguments.
    /// </summary>
    /// <exception cref="GhostScriptException"> Thrown when ghostscript. </exception>
    /// <param name="args"> The arguments. </param>
    /// <returns>
    ///  The GhostScript result code
    /// </returns>
    public int Execute(string[] args)
    {
      int result;
      IntPtr[] argumentHandlePointers;

      if (!string.IsNullOrEmpty(args[0]))
      {
        string[] workArgs;

        // ghostscript ignores the first argument in the array
        // therefore, if the array doesn't have a blank item as the first item, create one
        // saves callers having to think about dummy values etc
        workArgs = new string[args.Length + 1];
        args.CopyTo(workArgs, 1);
        workArgs[0] = string.Empty;
        args = workArgs;
      }

      Debug.Print(string.Join(" ", args));

      // set up pointers
      _argumentHandles = new GCHandle[args.Length];
      argumentHandlePointers = new IntPtr[args.Length];

      // initialize pointers
      for (int i = 0; i < args.Length; i++)
      {
        _argumentHandles[i] = GCHandle.Alloc(Encoding.ASCII.GetBytes(args[i]), GCHandleType.Pinned);
        argumentHandlePointers[i] = _argumentHandles[i].AddrOfPinnedObject();
      }
      _argumentPointersHandle = GCHandle.Alloc(argumentHandlePointers, GCHandleType.Pinned);

      // call ghostscript
      try
      {
        result = NativeMethods.gsapi_init_with_args(_ghostScriptInstance, args.Length, _argumentPointersHandle.AddrOfPinnedObject());
      }
      finally
      {
        this.FreeHandles();
      }

      if (result < (int)ErrorCode.Success)            // TODO: often if GhostScript fails because you've passed the wrong combination of params etc, it still returns zero... unhelpful
        throw new GhostScriptException(result, args);

      return result;
    }

  #endregion  Public Methods  

  #region  Protected Methods  

    /// <summary>
    ///  Free handles.
    /// </summary>
    protected virtual void FreeHandles()
    {
      if (_argumentHandles != null && _argumentHandles.Length != 0)
      {
        for (int i = 0; i < _argumentHandles.Length; i++)
          _argumentHandles[i].Free();
      }
      _argumentHandles = null;

      if (_argumentPointersHandle.IsAllocated)
        _argumentPointersHandle.Free();
    }

  #endregion  Protected Methods  
  }
  public enum PaperSize
  {
      Default,             // Not a real value, just an instruction to whatever is making use of this enum

      // US
      LedgerPortrait,
      Ledger,
      Legal,
      Letter,
      LetterSmall,
      ArchE,
      ArchD,
      ArchC,
      ArchB,
      ArchA,

      // ISO
      A0,
      A1,
      A2,
      A3,
      A4,
      A5,
      A6,
      A7,
      A8,
      A9,
      A10,
      B0,
      B1,
      B2,
      B3,
      B4,
      B5,
      B6,
      C0,
      C1,
      C2,
      C3,
      C4,
      C5,
      C6,

      // Other
      Foolscap,
      HalfLetter,
      Hagaki
  }
  public enum PdfTrimMode
  {
      PaperSize,
      TrimBox,
      CropBox
  }
  public enum ImageFormat
  {
      Unknown,
      BitmapMono,
      Bitmap8,
      Bitmap24,
      Jpeg,
      PngMono,
      Png8,
      Png24,
      TiffMono,
      Tiff24,
  }
  public enum GridFitMode
  {
      None,
      SkipPatentedInstructions,
      Topological,
      Mixed
  }
  public enum GhostScriptCommand
  {
      // http://www.ghostscript.com/doc/current/Use.htm#Parameter_switches

      // rendering
      ColorScreen,
      DitherPPI,
      Interpolate,
      NoInterpolate,
      TextAlphaBits,
      GraphicsAlphaBits,
      AlignToPixels,
      GridToFitTT,
      UseCIEColor,
      NoCIE,
      NoSubstituteDeviceColors,
      NoPSICC,
      NoTransparency,
      NoTN5044,
      DoPS,

      //page
      FixedMedia,
      FixedResolution,
      Orient1,
      DeviceWidthPoints,
      DeviceHeightPoint,
      DefaultPaperSize,

      // font
      DiskFonts,
      LocalFonts,
      NoCCFonts,
      NoFontMap,
      NoFontPath,
      NoPlatformFonts,
      NoNativeFontMap,
      FontMap,
      FontPath,
      SubstituteFont,
      OldCFFParser,

      // resource
      GenericResourceDirectory,
      FontResourceDirectory,

      // interaction
      Batch,
      NoPagePrompt,
      NoPause,
      NoPrompt,
      Quiet,
      ShortErrors,
      StandardOut,
      TTYPause,

      // device
      NoDisplay,
      Device,
      OutputFile,
      IgnoreMultipleCopies,

      // EPS
      EPSCrop,
      EPPSFitPage,
      NoEPS,

      // ICC
      DefaultGrayProfile,
      DefaultRGBProfile,
      DefaultCMYKProfile,
      DeviceNProfile,
      ProofProfile,
      DeviceLinkProfile,
      NamedProfile,
      OutputICCProfile,
      RenderIntent,
      GraphicICCProfile,
      GraphicIntent,
      ImageICCProfile,
      ImageIntent,
      TextICCProfile,
      TextIntent,
      OverrideICC,
      OverrideRI,
      SourceObjectICC,
      DeviceGrayToK,
      ICCProfilesDirectory,

      // Others
      DelayBind,
      PdfMarks,
      JobServer,
      NoBind,
      NoCache,
      NoGC,
      NoOuterSave,
      NoSafer,
      DelayedSave = NoSafer,
      Safer,
      Strict,
      SystemDictionaryWritable,

      // http://www.ghostscript.com/doc/current/Use.htm#PDF_switches

      // PDF
      FirstPage,
      LastPage,
      PDFFitPage,
      Printed,
      UseCropBox,
      UseTrimBox,
      PDFPassword,
      ShowAnnotations,
      ShowAcroForm,
      NoUserInit,
      RenderTTNotDef,

      // uncategorized
      Resolution,
      PaperSize,
      Silent,

      // not really named commands but added for consistence
      InputFile
  }
  public delegate int StdioCallBack(IntPtr handle, IntPtr strptr, int len);
  internal class NativeStructs
  {

      [StructLayout(LayoutKind.Sequential)]
      public struct GS_Revision
      {
          #region  Public Properties

          public int intRevision { get; set; }

          public int intRevisionDate { get; set; }

          public IntPtr strCopyright { get; set; }

          public IntPtr strProduct { get; set; }

          #endregion  Public Properties
      }
  }
  internal class NativeMethods
  {
      #region  Public Class Methods

      // void gsapi_delete_instance (void *instance);
      [DllImport("gsdll32.dll", EntryPoint = "gsapi_delete_instance")]
      public static extern void gsapi_delete_instance(IntPtr instance);

      // int gsapi_exit (void *instance);
      [DllImport("gsdll32.dll", EntryPoint = "gsapi_exit")]
      public static extern int gsapi_exit(IntPtr instance);

      // int gsapi_set_poll (void *instance, int(*poll_fn)(void *caller_handle));
      // int gsapi_set_display_callback (void *instance, display_callback *callback);
      // int gsapi_init_with_args (void *instance, int argc, char **argv);
      [DllImport("gsdll32.dll", EntryPoint = "gsapi_init_with_args")]
      public static extern int gsapi_init_with_args(IntPtr instance, int argc, IntPtr argv);

      //int gsapi_new_instance (void **pinstance, void *caller_handle);
      [DllImport("gsdll32.dll", EntryPoint = "gsapi_new_instance")]
      public static extern int gsapi_new_instance(out IntPtr pinstance, IntPtr caller_handle);

      // int gsapi_revision (gsapi_revision_t *pr, int len);
      [DllImport("gsdll32.dll", EntryPoint = "gsapi_revision")]
      public static extern int gsapi_revision(out NativeStructs.GS_Revision pr, int len);

      // int gsapi_run_string_begin (void *instance, int user_errors, int *pexit_code);
      // int gsapi_run_string_continue (void *instance, const char *str, unsigned int length, int user_errors, int *pexit_code);
      // int gsapi_run_string_end (void *instance, int user_errors, int *pexit_code);
      // int gsapi_run_string_with_length (void *instance, const char *str, unsigned int length, int user_errors, int *pexit_code);
      // int gsapi_run_string (void *instance, const char *str, int user_errors, int *pexit_code);
      // int gsapi_run_file (void *instance, const char *file_name, int user_errors, int *pexit_code);
      [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_file")]
      public static extern int gsapi_run_file(IntPtr instance, string file_name, int user_errors, int pexit_code);

      //int gsapi_set_stdio (void *instance, int(*stdin_fn)(void *caller_handle, char *buf, int len), int(*stdout_fn)(void *caller_handle, const char *str, int len), int(*stderr_fn)(void *caller_handle, const char *str, int len));
      [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_stdio")]
      public static extern int gsapi_set_stdio(IntPtr instance, StdioCallBack stdin_fn, StdioCallBack stdout_fn, StdioCallBack stderr_fn);

      #endregion  Public Class Methods

      // int gsapi_set_visual_tracer (gstruct vd_trace_interface_s *I);
  }
  public enum ErrorCode
  {

      /// <summary>Success</summary>
      Success = 0,

      /// <summary>Fatal Error</summary>
      FatalError = -100
  }
  public enum AntiAliasMode
  {
      None = 0,
      Low = 1,
      Medium = 2,
      High = 4,
  }
  public class GhostScriptException : Exception
  {
      #region  Public Constructors

      /// <summary>
      ///  Initializes a new instance of the GhostScriptException class.
      /// </summary>
      /// <param name="errorCode">  The error code. </param>
      public GhostScriptException(int errorCode)
          : this(errorCode, new string[] { })
      { }

      /// <summary>
      ///  Initializes a new instance of the GhostScriptException class.
      /// </summary>
      /// <param name="errorCode">  The error code. </param>
      /// <param name="arguments">  The arguments. </param>
      public GhostScriptException(int errorCode, string[] arguments)
          : this(errorCode, arguments, "Failed to process GhostScript command.")
      { }

      /// <summary>
      ///  Initializes a new instance of the GhostScriptException class.
      /// </summary>
      /// <param name="errorCode">  The error code. </param>
      /// <param name="arguments">  The arguments. </param>
      /// <param name="message">    The message. </param>
      public GhostScriptException(int errorCode, string[] arguments, string message)
          : this(errorCode, arguments, message, null)
      { }

      /// <summary>
      ///  Initializes a new instance of the GhostScriptException class.
      /// </summary>
      /// <param name="errorCode">      The error code. </param>
      /// <param name="arguments">  The arguments. </param>
      /// <param name="message">        The message. </param>
      /// <param name="innerException"> The inner exception. </param>
      public GhostScriptException(int errorCode, string[] arguments, string message, Exception innerException)
          : base(message, innerException)
      {
          this.Arguments = arguments;
          this.ErrorCode = errorCode;
      }

      #endregion  Public Constructors

      #region  Protected Constructors

      /// <summary>
      ///  Initializes a new instance of the GhostScriptException class.
      /// </summary>
      protected GhostScriptException()
      { }

      #endregion  Protected Constructors

      #region  Public Properties

      /// <summary>
      ///  Gets or sets the arguments associated the exception.
      /// </summary>
      /// <value>
      ///  The arguments.
      /// </value>
      public string[] Arguments { get; protected set; }

      /// <summary>
      ///  Gets or sets the error code.
      /// </summary>
      /// <value>
      ///  The error code.
      /// </value>
      public int ErrorCode { get; protected set; }

      #endregion  Public Properties
  }
  [TypeConverter(typeof(ExpandableObjectConverter))]
  public class Pdf2ImageSettings : INotifyPropertyChanged
  {
      #region  Private Member Declarations

      private AntiAliasMode _antiAliasMode;
      private int _dpi;
      private GridFitMode _gridFitMode;
      private ImageFormat _imageFormat;
      private PaperSize _paperSize;
      private PdfTrimMode _trimMode;

      #endregion  Private Member Declarations

      #region  Public Constructors

      public Pdf2ImageSettings()
      {
          this.Dpi = 150;
          this.GridFitMode = GridFitMode.Topological;
          this.AntiAliasMode = AntiAliasMode.High;
          this.ImageFormat = ImageFormat.Png24;
          this.PaperSize = PaperSize.Default;
          this.TrimMode = PdfTrimMode.CropBox;
      }

      #endregion  Public Constructors

      #region  Events

      public event PropertyChangedEventHandler PropertyChanged;

      #endregion  Events

      #region  Public Properties

      [DefaultValue(typeof(AntiAliasMode), "High")]
      public AntiAliasMode AntiAliasMode
      {
          get { return _antiAliasMode; }
          set
          {
              if (this.AntiAliasMode != value)
              {
                  _antiAliasMode = value;
                  this.OnPropertyChanged(new PropertyChangedEventArgs("AntiAliasMode"));
              }
          }
      }

      [DefaultValue(150)]
      public int Dpi
      {
          get { return _dpi; }
          set
          {
              if (this.Dpi != value)
              {
                  _dpi = value;
                  this.OnPropertyChanged(new PropertyChangedEventArgs("Dpi"));
              }
          }
      }

      [DefaultValue(typeof(GridFitMode), "Topological")]
      public GridFitMode GridFitMode
      {
          get { return _gridFitMode; }
          set
          {
              if (this.GridFitMode != value)
              {
                  _gridFitMode = value;
                  this.OnPropertyChanged(new PropertyChangedEventArgs("GridFitMode"));
              }
          }
      }

      [DefaultValue(typeof(ImageFormat), "Png24")]
      public ImageFormat ImageFormat
      {
          get { return _imageFormat; }
          set
          {
              if (this.ImageFormat != value)
              {
                  _imageFormat = value;
                  this.OnPropertyChanged(new PropertyChangedEventArgs("ImageFormat"));
              }
          }
      }

      [DefaultValue(typeof(PaperSize), "Default")]
      public PaperSize PaperSize
      {
          get { return _paperSize; }
          set
          {
              if (this.PaperSize != value)
              {
                  _paperSize = value;
                  this.OnPropertyChanged(new PropertyChangedEventArgs("PaperSize"));
              }
          }
      }

      [DefaultValue(typeof(PdfTrimMode), "CropBox")]
      public PdfTrimMode TrimMode
      {
          get { return _trimMode; }
          set
          {
              if (this.TrimMode != value)
              {
                  _trimMode = value;
                  this.OnPropertyChanged(new PropertyChangedEventArgs("TrimMode"));
              }
          }
      }

      #endregion  Public Properties

      #region  Protected Methods

      protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
      {
          if (this.PropertyChanged != null)
              this.PropertyChanged(this, e);
      }

      #endregion  Protected Methods
  }
  public class Pdf2Image
  {
      #region  Private Member Declarations

      private string _pdfFileName;
      private int _pdfPageCount;

      #endregion  Private Member Declarations

      #region  Public Constructors

      public Pdf2Image()
      {
          this.Settings = new Pdf2ImageSettings();
      }

      public Pdf2Image(string pdfFileName)
          : this()
      {
          if (string.IsNullOrEmpty(pdfFileName))
              throw new ArgumentNullException("pdfFileName");

          if (!File.Exists(pdfFileName))
              throw new FileNotFoundException();

          this.PdfFileName = pdfFileName;
      }

      public Pdf2Image(string pdfFileName, Pdf2ImageSettings settings)
          : this(pdfFileName)
      {
          if (settings == null)
              throw new ArgumentNullException("setting");

          this.Settings = settings;
      }

      #endregion  Public Constructors

      #region  Public Methods

      public void ConvertPdfPageToImage(string outputFileName, int pageNumber)
      {
          if (pageNumber < 1 || pageNumber > this.PageCount)
              throw new ArgumentException("Page number is out of bounds", "pageNumber");

          using (GhostScriptAPI api = new GhostScriptAPI())
              api.Execute(this.GetConversionArguments(this._pdfFileName, outputFileName, pageNumber, this.PdfPassword, this.Settings));
      }

      public Bitmap GetImage()
      {
          return this.GetImage(1);
      }

      public Bitmap GetImage(int pageNumber)
      {
          Bitmap result;
          string workFile;

          if (pageNumber < 1 || pageNumber > this.PageCount)
              throw new ArgumentException("Page number is out of bounds", "pageNumber");

          workFile = Path.GetTempFileName();

          try
          {
              this.ConvertPdfPageToImage(workFile, pageNumber);
              using (FileStream stream = new FileStream(workFile, FileMode.Open, FileAccess.Read))
                  result = new Bitmap(stream);
          }
          finally
          {
              File.Delete(workFile);
          }

          return result;
      }

      public Bitmap[] GetImages()
      {
          return this.GetImages(1, this.PageCount);
      }

      public Bitmap[] GetImages(int startPage, int lastPage)
      {
          List<Bitmap> results;

          if (startPage < 1 || startPage > this.PageCount)
              throw new ArgumentException("Start page number is out of bounds", "startPage");

          if (lastPage < 1 || lastPage > this.PageCount)
              throw new ArgumentException("Last page number is out of bounds", "lastPage");
          else if (lastPage < startPage)
              throw new ArgumentException("Last page cannot be less than start page", "lastPage");

          results = new List<Bitmap>();
          for (int i = startPage; i <= lastPage; i++)
              results.Add(this.GetImage(i));

          return results.ToArray();
      }

      #endregion  Public Methods

      #region  Public Properties

      [DefaultValue(0)]
      public int PageCount
      {
          get
          {
              if (_pdfPageCount == 0 && !string.IsNullOrEmpty(_pdfFileName))
                  _pdfPageCount = this.GetPdfPageCount(_pdfFileName);

              return _pdfPageCount;
          }
      }

      [DefaultValue("")]
      public string PdfFileName
      {
          get { return _pdfFileName; }
          set { _pdfFileName = value; }
      }

      [DefaultValue("")]
      public string PdfPassword { get; set; }

      public Pdf2ImageSettings Settings { get; set; }

      #endregion  Public Properties

      #region  Private Methods

      private int GetPdfPageCount(string fileName)
      {
          // http://stackoverflow.com/questions/320281/determine-number-of-pages-in-a-pdf-file-using-c-net-2-0

          return Regex.Matches(File.ReadAllText(fileName), @"/Type\s*/Page[^s]").Count;
      }

      #endregion  Private Methods

      #region  Protected Methods

      protected virtual IDictionary<GhostScriptCommand, object> GetConversionArguments(string pdfFileName, string outputImageFileName, int pageNumber, string password, Pdf2ImageSettings settings)
      {
          Dictionary<GhostScriptCommand, object> arguments;

          arguments = new Dictionary<GhostScriptCommand, object>();

          // basic GhostScript setup
          arguments.Add(GhostScriptCommand.Silent, null);
          arguments.Add(GhostScriptCommand.Safer, null);
          arguments.Add(GhostScriptCommand.Batch, null);
          arguments.Add(GhostScriptCommand.NoPause, null);

          // specify the output
          arguments.Add(GhostScriptCommand.Device, GhostScriptAPI.GetDeviceName(settings.ImageFormat));
          arguments.Add(GhostScriptCommand.OutputFile, outputImageFileName);

          // page numbers
          arguments.Add(GhostScriptCommand.FirstPage, pageNumber);
          arguments.Add(GhostScriptCommand.LastPage, pageNumber);

          // graphics options
          //arguments.Add(GhostScriptCommand.UseCIEColor, null);

          if (settings.AntiAliasMode != AntiAliasMode.None)
          {
              arguments.Add(GhostScriptCommand.TextAlphaBits, settings.AntiAliasMode);
              arguments.Add(GhostScriptCommand.GraphicsAlphaBits, settings.AntiAliasMode);
          }

          arguments.Add(GhostScriptCommand.GridToFitTT, settings.GridFitMode);

          // image size
          if (settings.TrimMode != PdfTrimMode.PaperSize)
              arguments.Add(GhostScriptCommand.Resolution, settings.Dpi.ToString());

          switch (settings.TrimMode)
          {
              case PdfTrimMode.PaperSize:
                  if (settings.PaperSize != PaperSize.Default)
                  {
                      arguments.Add(GhostScriptCommand.FixedMedia, true);
                      arguments.Add(GhostScriptCommand.PaperSize, settings.PaperSize);
                  }
                  break;
              case PdfTrimMode.TrimBox:
                  arguments.Add(GhostScriptCommand.UseTrimBox, true);
                  break;
              case PdfTrimMode.CropBox:
                  arguments.Add(GhostScriptCommand.UseCropBox, true);
                  break;
          }

          // pdf password
          if (!string.IsNullOrEmpty(password))
              arguments.Add(GhostScriptCommand.PDFPassword, password);

          // pdf filename
          arguments.Add(GhostScriptCommand.InputFile, pdfFileName);

          return arguments;
      }

      #endregion  Protected Methods
  }
}
