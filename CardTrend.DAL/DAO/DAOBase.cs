using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Objects.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL
{
    public class DAOBase
    {
        public static string BuildSqlCommand(string sql, List<SqlParameter> parameters, bool formatSQL = false)
        {
            var paramDeclare = new StringBuilder();

            if (formatSQL == false)
            {
                paramDeclare.Append(sql);
            }


            // append parameter declare: {0}, {1}, {2}
            for (int i = 0; i < parameters.Count; i++)
            {
                paramDeclare.AppendFormat(" {0}", parameters[i].ParameterName);
                if (parameters[i].Direction == ParameterDirection.InputOutput)
                    paramDeclare.Append(" out ");

                if (i < parameters.Count - 1)
                    paramDeclare.Append(",");
            }

            if (formatSQL)
            {
                return string.Format(sql, paramDeclare);
            }

            return paramDeclare.ToString();
        }
        public static string BuildSqlCommandWithRrn(string sql, List<SqlParameter> parameters)
        {
            var paramDeclare = new StringBuilder();

            paramDeclare.Append("exec @RETURN_VALUE = ");

            paramDeclare.Append(sql);

            // append parameter declare: {0}, {1}, {2}
            for (int i = 0; i < parameters.Count - 1; i++)// Except RETURN_VALUE has added above
            {
                paramDeclare.AppendFormat(" {0}", parameters[i].ParameterName);
                if (parameters[i].Direction == ParameterDirection.Output)
                    paramDeclare.Append(" out ");

                if (i < parameters.Count - 2)
                    paramDeclare.Append(",");
            }

            return paramDeclare.ToString();
        }
        public static List<SqlParameter> BuildParameterList(object[] objectList, params string[] paramNameList)
        {
            return paramNameList.Select((t, i) => new SqlParameter()
            {
                ParameterName = t,
                Value = objectList[i] ?? DBNull.Value               
            }).ToList();

            //new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output},
        }
        public static List<SqlParameter> BuildParameterListWithRrn(object[] objectList, params string[] paramNameList)
        {
            var result = paramNameList.Select((t, i) => new SqlParameter()
            {
                ParameterName = t,
                Value = objectList[i] ?? DBNull.Value
            }).ToList();

            result.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt) { Direction = ParameterDirection.Output });
            
            return result;
        }
        public static List<SqlParameter> BuildParameterStructuredParam(object[] objectList, object[] structuredParam, params string[] paramNameList)
        {
            var result = paramNameList.Select((t, i) => new SqlParameter()
            {
                ParameterName = t,
                Value = objectList[i] ?? DBNull.Value
            }).ToList();
            if (structuredParam.Count() > 0)
            {
                for (int i = 0; i < structuredParam.Count(); i++)
                {
                    var columnInfo = (ColumnInfo)structuredParam[i];
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.Structured) { SqlValue =  columnInfo.Value, TypeName = columnInfo.DataType});
                }
            }

            return result;
        }
        public static List<SqlParameter> BuildParameterListWithOutPutAndRrn(object[] objectList,object[] objectOutPutParam, params string[] paramNameList)
        {
            var result = paramNameList.Select((t, i) => new SqlParameter()
            {
                ParameterName = t,
                Value = objectList[i] ?? DBNull.Value
            }).ToList();
            if(objectOutPutParam.Count() > 0)
            {
                for (int i = 0; i < objectOutPutParam.Count();i++ )
                {
                    var columnInfo = (ColumnInfo)objectOutPutParam[i];
                    if(columnInfo.DataType.ToLower() == "int")
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.Int) { Direction = ParameterDirection.Output });
                    if (columnInfo.DataType.ToLower() == "bigint")
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.BigInt) { Direction = ParameterDirection.Output });
                    else if(columnInfo.DataType.ToLower() == "varchar")
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.VarChar, columnInfo.ColLength) { Direction = ParameterDirection.Output });
                }
            }
            result.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt) { Direction = ParameterDirection.Output });

            return result;
        }
        public static List<SqlParameter> BuildParameterListWithOutPut(object[] objectList, object[] objectOutPutParam, params string[] paramNameList)
        {
            var result = paramNameList.Select((t, i) => new SqlParameter()
            {
                ParameterName = t,
                Value = objectList[i] ?? DBNull.Value
            }).ToList();
            if (objectOutPutParam.Count() > 0)
            {
                for (int i = 0; i < objectOutPutParam.Count(); i++)
                {
                    var columnInfo = (ColumnInfo)objectOutPutParam[i];
                    if (columnInfo.DataType.ToLower() == "int")
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.Int) { Direction = ParameterDirection.Output });
                    if (columnInfo.DataType.ToLower() == "bigint")
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.BigInt) { Direction = ParameterDirection.Output });
                    else if (columnInfo.DataType.ToLower() == "varchar")
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.VarChar, columnInfo.ColLength) { Direction = ParameterDirection.Output });
                    else if (columnInfo.DataType.ToLower() == "decimal" || columnInfo.DataType.ToLower() == "money")
                        result.Add(new SqlParameter(columnInfo.FieldName, SqlDbType.Money) { Direction = ParameterDirection.Output });
                }
            }

            return result;
        }
    }
}
