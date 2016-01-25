using Cake.Common;
using Cake.Core;
using Code.Cake;
using System;
using System.Linq;

namespace CodeCakeBuilder
{
    public class Encrypt : CodeCakeHost
    {
        public Encrypt()
        {
            Task( "Default" )
                .Does( () =>
                {
                    string targetPath = Cake.Arguments.GetArgument("targetPath");
                    string outPath = Cake.Arguments.GetArgument("outPath");
                    string passphrase = Cake.Arguments.GetArgument("passphrase");

                    if( String.IsNullOrEmpty( targetPath ) || String.IsNullOrEmpty( passphrase ) )
                    {
                        throw new ArgumentException( "targetPath or passphrase expected. Use command : CodeCakeBuilder Encrypt -targetPath=\"Path\" -passphrase=\"passphrase\"" );
                    }
                    if( String.IsNullOrEmpty( outPath ) )
                    {
                        outPath = Environment.CurrentDirectory + "\\" + targetPath.Split( '\\' ).Last() + ".enc";
                    }
                    Console.WriteLine( "Encryting " + targetPath + " to " + outPath );
                    Cake.SecureFileCrypt( targetPath, outPath, passphrase );
                } );

        }
    }
}

