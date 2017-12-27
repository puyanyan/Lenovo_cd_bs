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

namespace Xinning.Lenovo.VMI
{
    public class DefinitionItemDal
    {

        public static IDataAccess myDataAccess = IApplication.DefaultDataAccess();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DefinitionItemInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_predefine_item(");
            strSql.Append("L_prei_prehid,[S_prei_key],S_prei_value,S_prei_memo,L_prei_effic)");
            strSql.Append(" values (");
            strSql.Append("@L_prei_prehid,@S_prei_key,@S_prei_value,@S_prei_memo,@L_prei_effic)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@L_prei_prehid", SqlDbType.Int, 4),
                new SqlParameter("@S_prei_key", SqlDbType.NVarChar, 50),
                new SqlParameter("@S_prei_value", SqlDbType.NVarChar, 256), 
                new SqlParameter("@S_prei_memo", SqlDbType.NVarChar, 256), 
                new SqlParameter("@L_prei_effic", SqlDbType.SmallInt, 2) };

            parameters[0].Value = model.headId;
            parameters[1].Value = model.Key;
            parameters[2].Value = model.value;
            parameters[3].Value = model.memo;
            parameters[4].Value = model.IsEffective;

            DataTable dt = myDataAccess.ExecuteDataTable(strSql.ToString(), parameters); ;
            if (dt.Rows.Count == 0 || dt == null)
            {
                return -1;
            }
            else
            {
                return System.Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(DefinitionItemInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_predefine_item set ");
            strSql.Append("[S_prei_key]=@S_prei_key,");
            strSql.Append("[S_prei_value]=@S_prei_value,");
            strSql.Append("S_prei_memo=@S_prei_memo,");
            strSql.Append("L_prei_effic=@L_prei_effic");
            strSql.Append(" where L_prei_prehid=@L_prei_prehid and L_prei_id=@L_prei_id ");
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@L_prei_id", SqlDbType.Int, 4),
                new SqlParameter("@L_prei_prehid", SqlDbType.Int, 4),
                new SqlParameter("@S_prei_key", SqlDbType.NVarChar, 50),
                new SqlParameter("@S_prei_value", SqlDbType.NVarChar, 256), 
                new SqlParameter("@S_prei_memo", SqlDbType.NVarChar, 256),
                new SqlParameter("@L_prei_effic", SqlDbType.SmallInt, 2)};

            parameters[0].Value = model.id;
            parameters[1].Value = model.headId;
            parameters[2].Value = model.Key;
            parameters[3].Value = model.value;
            parameters[4].Value = model.memo;
            parameters[5].Value = model.IsEffective;

            myDataAccess.ExecuteNoneQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, int headId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete tb_predefine_item ");
            strSql.Append(" where L_prei_prehid=@L_prei_prehid and L_prei_id=@L_prei_id ");
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@L_prei_prehid", SqlDbType.Int, 4), 
                new SqlParameter("@L_prei_id", SqlDbType.Int, 4)};

            parameters[0].Value = headId;
            parameters[1].Value = id;

            myDataAccess.ExecuteNoneQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除配置种类一批数据
        /// </summary>
        public void Delete(int headId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete tb_predefine_item ");
            strSql.Append(" where L_prei_prehid=@L_prei_prehid ");
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@L_prei_prehid", SqlDbType.Int, 4) };
            parameters[0].Value = headId;

            myDataAccess.ExecuteNoneQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DefinitionItemInfo GetModel(int id, int headId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ,tb_predefine_head.[S_preh_key] as headkey from tb_predefine_item ");
            strSql.Append(" inner join  tb_predefine_head on tb_predefine_head.L_preh_id = tb_predefine_item.L_prei_prehid  ");
            strSql.Append(" where tb_predefine_item.L_prei_prehid=@L_prei_prehid and tb_predefine_item.L_prei_id=@L_prei_id ");
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@L_prei_prehid", SqlDbType.Int, 4), 
                new SqlParameter("@L_prei_id", SqlDbType.Int, 4)};

            parameters[0].Value = headId;
            parameters[1].Value = id;

            DefinitionItemInfo model = new DefinitionItemInfo();
            DataSet ds = myDataAccess.ExecuteDataSet(strSql.ToString(), parameters);
            if ((ds != null) && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["L_prei_id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["L_prei_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L_prei_prehid"].ToString() != "")
                {
                    model.headId = int.Parse(ds.Tables[0].Rows[0]["L_prei_prehid"].ToString());
                }
                model.Key = ds.Tables[0].Rows[0]["S_prei_key"].ToString();
                model.value = ds.Tables[0].Rows[0]["S_prei_value"].ToString();
                model.memo = ds.Tables[0].Rows[0]["S_prei_memo"].ToString();
                model.headKey = ds.Tables[0].Rows[0]["headkey"].ToString();
                if (ds.Tables[0].Rows[0]["L_prei_effic"].ToString() != "")
                {
                    model.IsEffective = int.Parse(ds.Tables[0].Rows[0]["L_prei_effic"].ToString());
                }
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
            strSql.Append("select tb_predefine_item.* ,case  L_prei_effic  when 1 then \'是\' else \'否\' end    as  iseff  ");
            strSql.Append(" FROM tb_predefine_item ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return myDataAccess.ExecuteDataSet(strSql.ToString());
        }

        public static DataTable GetQueryByData(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  b.*    FROM tb_predefine_head a  inner join tb_predefine_item b  ");
            strSql.Append(" on  a.L_preh_id = b.L_prei_prehid   ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + " and b.L_prei_effic =1 ");
            }
            else
                strSql.Append(" where  b.L_prei_effic =1 ");

            return myDataAccess.ExecuteDataTable(strSql.ToString());
        }

    }




}
