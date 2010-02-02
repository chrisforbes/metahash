﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;

namespace metahash_client
{
	class Program
	{
		const string fileHashUri = "http://metahash.com/file/";

		static void Main(string[] args)
		{
			foreach (var f in args)
			{
				var hash = CalculateSHA1(f);

				using (var wc = new WebClient())
				{
					var content = wc.DownloadString(fileHashUri + hash);
					Console.WriteLine(content);
				}
			}
		}

		static string CalculateSHA1(string filename)
		{
			using (var csp = SHA1.Create())
				return new string(csp.ComputeHash(File.ReadAllBytes(filename))
					.SelectMany(a => a.ToString("x2")).ToArray());
		}
	}
}
