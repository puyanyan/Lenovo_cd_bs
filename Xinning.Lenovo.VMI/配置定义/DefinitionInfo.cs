using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;


/// <summary>
/// 实体类tb_levo_definition 。(属性说明自动提取数据库字段的描述信息)
/// </summary>
namespace Xinning.Lenovo.VMI
{
	public class DefinitionInfo
	{
		
		public DefinitionInfo()
		{
		}
		
		private int _id;
		private string _key;
		private string _value;
		private string _memo;
		/// <summary>
		///
		/// </summary>
		public int id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}
		/// <summary>
		///
		/// </summary>
		public string Key
		{
			get
			{
				return _key;
			}
			set
			{
				_key = value;
			}
		}
		/// <summary>
		///
		/// </summary>
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}
		/// <summary>
		///
		/// </summary>
		public string memo
		{
			get
			{
				return _memo;
			}
			set
			{
				_memo = value;
			}
		}
		
		
	}
	
	
}
