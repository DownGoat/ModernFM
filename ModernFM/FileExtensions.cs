using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static class FileExtensions
{
    static List<String> executable = new List<String>
    {
        ".exe", ".dll", ".bat", ".bin", ".cmd", ".com", ".cpl", ".gadget", ".ins", ".inx",
        ".isu", ".job", ".jse", ".lnk", ".msc", ".msi", ".msp", ".mst", ".paf", ".pif", ".ps1",
        ".reg", ".rgs", ".scr", ".sct", ".shb", ".shs", ".u3p", ".vb", ".vbe", ".vbs", ".vbscript",
        ".ws", ".wsf", ".wsh"
    };

    static List<String> media = new List<String>
    {
        ".3g2",".3gp",".3gp2",".3gpp",".3gpp2",".aac",".acp",".adts",".aif",".aifc",".aiff",".amc",
        ".amr",".asf",".asx",".au",".avi",".awb",".bwf",".caf",".cda",".cdda",".cel",".cue",".dif",
        ".divx",".dv",".flc",".fli",".flv",".gsm",".ivf",".kar",".m15",".m1a",".m1s",".m1v",".m2v",
        ".m3u",".m4a",".m4b",".m4e",".m4p",".m4v",".m75",".mid",".midi",".mjpg",".mov",".mp1",".mp2",
        ".mp3",".mp4",".mpa",".mpe",".mpeg",".mpg",".mpga",".mpm",".mps",".mpv",".mpv2",".mqv",".mv",
        ".ogg",".ogm",".pls",".qcp",".qt",".qtc",".qtl",".ra",".ram",".rm",".rmi",".rmm",".rmp",".rmvb",
        ".rnx",".rp",".rt",".rts",".rtsp",".rv",".sd2",".sdp",".sdv",".sf",".smf",".smi",".smil",".snd",
        ".ssm",".swa",".swf",".ulw",".vfw",".wav",".wax",".wm",".wma",".wmf",".wmp",".wmv",".wmx",".wvx",".xpl"
    };

    static List<String> archive = new List<String>
    {
        ".gz", ".tar", ".zip", ".tgz", ".bz2", ".rar", ".tar.gz", ".7z"
    };


    public static bool IsExecutalbe(string extension)
    {
        return executable.Contains(extension.ToLower());
    }

    public static bool IsMedia(string extension)
    {
        return media.Contains(extension.ToLower());
    }

    public static bool IsArchive(string extension)
    {
        return archive.Contains(extension.ToLower());
    }
}

