﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TrashBin
{
	class GetBinData
	{
		public string _cb_size;
		public string _file_size;
		public int _file_sizeMB;
		public string _num_items;
		public int _num_itemsMB;



		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
		public struct SHQUERYRBINFO
		{
			public Int32 cbSize;
			public UInt64 i64Size;
			public UInt64 i64NumItems;
		}
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		public static extern int SHQueryRecycleBin([MarshalAs(UnmanagedType.LPTStr)]String pszRootPath,ref SHQUERYRBINFO pSHQueryRBInfo);
		public void GetSize()
		{
			SHQUERYRBINFO bb_Query = new SHQUERYRBINFO();
			bb_Query.cbSize = Marshal.SizeOf(bb_Query.GetType());

			SHQueryRecycleBin(null, ref bb_Query);
			_cb_size = "CB Size  :  " + bb_Query.cbSize;
			_num_items = bb_Query.i64NumItems + Language.Translate("files");
			_num_itemsMB = Convert.ToInt32(bb_Query.i64NumItems);
			_file_size = bb_Query.i64Size + Language.Translate("byte");  
			if (bb_Query.i64Size >= 1024)
			{
				_file_size = bb_Query.i64Size / 1024 + Language.Translate("KB");
				if (bb_Query.i64Size >= 1048576)
				{
					_file_size = bb_Query.i64Size / 1048576 + Language.Translate("MB");
					_file_sizeMB = Convert.ToInt32(bb_Query.i64Size / 1048576);
					if (bb_Query.i64Size >= 1073741824)
					{
						string sizeGB = (Convert.ToDouble(bb_Query.i64Size) / 1073741824).ToString();
						_file_size = sizeGB.Substring(0, sizeGB.IndexOf(",") + 3) + Language.Translate("GB");
					}
				}
			}
		}
		public int GetMaxSize()
		{
			string ss = @"Software\Microsoft\Windows\CurrentVersion\Explorer\BitBucket\Volume";
			RegistryKey key = Registry.CurrentUser.OpenSubKey(ss);
			string[] subNames = key.GetSubKeyNames();
			double sum = 0;
			foreach (var subName in subNames)
			{
				string newpath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\BitBucket\Volume\" + subName;
				RegistryKey newkey = Registry.CurrentUser.OpenSubKey(newpath);
				string value = newkey.GetValue("MaxCapacity").ToString();
				int newvalue = Convert.ToInt32(value);
				sum += newvalue;
			}

			return Convert.ToInt32(sum);
		}
	}
}
