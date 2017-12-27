using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;


/// <summary>
/// 实体类tb_levo_definitionItem 。(属性说明自动提取数据库字段的描述信息)
/// </summary>
namespace Xinning.Lenovo.VMI
{
	public class DefinitionItemInfo
	{
		
		public DefinitionItemInfo()
		{
		}
		
		private int _id;
		private int _headid;
		private string _key;
		private string _value;
		private string _memo;
		private int _iseffective;
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
		public int headId
		{
			get
			{
				return _headid;
			}
			set
			{
				_headid = value;
			}
		}
		
		private string _headKey;
		/// <summary>
		///
		/// </summary>
		public string headKey
		{
			get
			{
				return _headKey;
			}
			set
			{
				_headKey = value;
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
		public string value
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
		/// <summary>
		///
		/// </summary>
		public int IsEffective
		{
			get
			{
				return _iseffective;
			}
			set
			{
				_iseffective = value;
			}
		}
		
		
	}
	
	
	
}
