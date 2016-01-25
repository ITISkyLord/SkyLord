using Cake.Core;
using Cake.Core.Diagnostics;
using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Code.Cake
{
    public static class DNXSupportOTD
    {
        static DNXRuntimeInformation _dnxRTI;

        /// <summary>
        /// Gets the DNX runtime information. Never null but <see cref="DNXRuntimeInformation.IsValid"/>
        /// may be false.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <returns>A runtime information object.</returns>
        public static DNXRuntimeInformation GetDNXRuntimeInformation( this ICakeContext context )
        {
            return _dnxRTI ?? (_dnxRTI = LoadDNXRuntimeInformation( context ));
        }

        public static void DNUPublish( this ICakeContext context, Action<DNUPublishSettings> config )
        {
            var c = new DNUPublishSettings();
            config( c );
            var b = new StringBuilder();
            b.Append( "dnu publish " );
            c.ToString( b );
            RunSuccessfullCmd( context, b.ToString() );
        }


        /// <summary>
        /// Runs cmd.exe with a command line and throws an exception if the command exits with a result that is not 0.
        /// This is currently private but may be exposed once.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="commandLine">The command line to execute.</param>
        public static void RunSuccessfullCmd( this ICakeContext context, string commandLine )
        {
            int r = RunCmd( context, commandLine );
            if( r != 0 ) throw new Exception( "An error occured in command: " + commandLine );
        }

        /// <summary>
        /// Runs cmd.exe with a command line and returns the process result value.
        /// This is currently private but may be exposed once.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="commandLine">The command line to execute.</param>
        /// <param name="output">Optional standard output lines collector.</param>
        public static int RunCmd( this ICakeContext context, string commandLine, Action<string> output = null )
        {
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = @"cmd.exe";
            cmdStartInfo.Arguments = "/C " + commandLine;
            cmdStartInfo.RedirectStandardOutput = true;
            cmdStartInfo.RedirectStandardError = true;
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            cmdStartInfo.CreateNoWindow = true;

            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.ErrorDataReceived += ( o, e ) => { if( !string.IsNullOrEmpty( e.Data ) ) context.Log.Error( e.Data ); };
            cmdProcess.OutputDataReceived += ( o, e ) =>
            {
                if( e.Data != null )
                {
                    context.Log.Information( e.Data );
                    if( output != null ) output( e.Data );
                }
            };
            cmdProcess.Start();
            cmdProcess.BeginErrorReadLine();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.WaitForExit();
            return cmdProcess.ExitCode;
        }

     
        /// <summary>
        /// Always create a <see cref="DNXRuntimeInformation"/> object that may be not valid.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        static DNXRuntimeInformation LoadDNXRuntimeInformation( ICakeContext context )
        {
            var output = new List<string>();
            if( RunCmd( context, "where dnx", output.Add ) != 0 ) return new DNXRuntimeInformation( null );
            return new DNXRuntimeInformation( output[0] );
        }

    }
}