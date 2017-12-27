using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using Bin.FrameWork;
using System.Text;
using System.Data.SqlClient;
using Bin.FrameWork.DataAccess;


/// <summary>
/// 配置参数定义
/// </summary>
/// <remarks></remarks>
namespace Xinning.Lenovo.VMI
{
	public class DefinitionDal
	{
        public static IDataAccess myDataAccess = IApplication.DefaultDataAccess();
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DefinitionInfo model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into tb_predefine_head(");
			strSql.Append("[S_preh_key],[S_preh_value],S_preh_memo)");
			strSql.Append(" values (");
			strSql.Append("@S_preh_key,@S_preh_value,@S_preh_memo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[] {new SqlParameter("@S_preh_key", SqlDbType.NVarChar, 50), new SqlParameter("@S_preh_value", SqlDbType.NVarChar, 256), new SqlParameter("@S_preh_memo", SqlDbType.NVarChar, 256)};
			
			parameters[0].Value = model.Key;
			parameters[1].Value = model.Value;
			parameters[2].Value = model.memo;
			
			DataTable dt =  myDataAccess.ExecuteDataTable(strSql.ToString(), parameters);;
			if (dt.Rows.Count == 0 || dt == null)
			{
				return - 1;
			}
			else
			{
				return System.Convert.ToInt32(dt.Rows[0][0].ToString());
			}
			
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DefinitionInfo model)
		{
			
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update tb_predefine_head set ");
			strSql.Append("[S_preh_key]=@S_preh_key,");
			strSql.Append("[S_preh_value]=@S_preh_value,");
			strSql.Append("S_preh_memo=@S_preh_memo");
			strSql.Append(" where L_preh_id=@L_preh_id ");
			SqlParameter[] parameters = new SqlParameter[] {new SqlParameter("@L_preh_id", SqlDbType.Int, 4), new SqlParameter("@S_preh_key", SqlDbType.NVarChar, 50), new SqlParameter("@S_preh_value", SqlDbType.NVarChar, 256), new SqlParameter("@S_preh_memo", SqlDbType.NVarChar, 256)};
			
			parameters[0].Value = model.id;
			parameters[1].Value = model.Key;
			parameters[2].Value = model.Value;
			parameters[3].Value = model.memo;
			
			  myDataAccess.ExecuteNoneQuery(strSql.ToString(), parameters);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete tb_predefine_head ");
			strSql.Append(" where L_preh_id=@L_preh_id ");
			SqlParameter[] parameters = new SqlParameter[] {new SqlParameter("@L_preh_id", SqlDbType.Int, 4)};
			
			parameters[0].Value = id;
			
			  myDataAccess.ExecuteNoneQuery(strSql.ToString(), parameters);
		}
		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DefinitionInfo GetModel(int id)
		{
			
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 tb_predefine_head. *  from tb_predefine_head ");
			strSql.Append(" where L_preh_id=@L_preh_id ");
			SqlParameter[] parameters = new SqlParameter[] {new SqlParameter("@L_preh_id", SqlDbType.Int, 4)};
			
			parameters[0].Value = id;
			
			DefinitionInfo model = new DefinitionInfo();
			DataSet ds =  myDataAccess.ExecuteDataSet(strSql.ToString(), parameters);
			if ((ds != null)&& ds.Tables[0].Rows.Count > 0)
			{
				
				if (ds.Tables[0].Rows[0]["L_preh_id"].ToString() != "")
				{
					model.id = int.Parse(ds.Tables[0].Rows[0]["L_preh_id"].ToString());
				}
				model.Key = ds.Tables[0].Rows[0]["S_preh_key"].ToString();
				model.Value = ds.Tables[0].Rows[0]["S_preh_value"].ToString();
				model.memo = ds.Tables[0].Rows[0]["S_preh_memo"].ToString();
				return model;
			}
			else
			{
				return null;
			}
			
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *  FROM tb_predefine_head");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return   myDataAccess.ExecuteDataSet(strSql.ToString());
		}
		
		public DataSet GetDefintionInfoList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select  a.*,a.[S_preh_key] as  itemvalue  FROM tb_predefine_head a  inner join tb_predefine_item b  ");
            strSql.Append(" on  a.L_preh_id = b.L_prei_prehid   ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return   myDataAccess.ExecuteDataSet(strSql.ToString());
		}
		
	}
	
	
	
	
}
