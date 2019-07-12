using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FileApi.Utility
{
    public class VlcUtility
    {
        public async Task Transcode()
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                // Default installation path of VideoLAN.LibVLC.Windows
                var libDirectory =
                    new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
                var destination = Path.Combine(currentDirectory + "\\wwwroot");
                using (var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory))
                {

                    var mediaOptions = new[]
                    {
                    ":sout=#transcode{vcodec=h264, vb=300, venc=x264, fps=15, width=352, height=288, acodec=mp3, aenc=ffmpeg, samplerate=44100, threads=2}:standard{access=file,mux=mp4,dst="+destination+"\\6.mp4"
                };
                    //var mediaOptions = new[]
                    //{
                    //    ":sout=#transcode{vcodec=theo}:std{access=http,mux=ogg,dst=:8090}",
                    //     ":sout-keep"
                    //};
                    //":sout=#transcode{vcodec=h264,vb072}:standard{access=file,mux=mp4,dst=D:\\Work\\Test\\Video\\Video\\bin\\Debug\\wwwroot\\6.mp4"

                    //mediaPlayer.SetMedia(new Uri("http://58.222.132.163:17000/pag/172.21.41.136/7302/009120/0/MAIN/TCP/live.m3u8"),
                    //                mediaOptions);
                    // 打开文件
                    FileStream fileStream = new FileStream(destination + "\\4.mp4", FileMode.Open, FileAccess.Read, FileShare.Read);
                    // 读取文件的 byte[]
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes, 0, bytes.Length);
                    fileStream.Close();
                    // 把 byte[] 转换成 Stream
                    Stream stream = new MemoryStream(bytes);
                    mediaPlayer.SetMedia(stream,
                                    mediaOptions);
                    TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
                    mediaPlayer.EndReached += (sender, e) => { ThreadPool.QueueUserWorkItem(_ => tcs.TrySetResult(true)); };
                    mediaPlayer.Play();
                    await tcs.Task;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
