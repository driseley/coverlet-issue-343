using Dapper;
using DataAbstractions.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
	public class DemoAsyncClass
    {
        private readonly IConnectionFactory _connectionFactory;

        public DemoAsyncClass(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public interface IConnectionFactory : IDisposable
        {
            IDataAccessor GetDataAccessor();
        }

        protected async Task<T> DataAccessorAsync<T>(Func<IDataAccessor, Task<T>> getResultFunction)
        {
            using (var dataAccessor = _connectionFactory.GetDataAccessor())
            {
                var result = await getResultFunction(dataAccessor);
                return result;
            }
        }

        public async Task<IEnumerable<Job>> InvokeFunction(string param1 = null, string param2 = null)
        {
            return await DataAccessorAsync(d => GetJobs(d, param1, param2));
        }

        private static async Task<IEnumerable<Job>> GetJobs(IDataAccessor dataAccessor, string param1 = null, string param2 = null)
        {
            var parameters = new DynamicParameters();
            if (param1 != null)
            {
                parameters.Add("param1", param1);
            }
            if (param2 != null)
            {
                parameters.Add("param2", param2);
            }
            parameters.Add("Emsg", dbType: DbType.String, direction: ParameterDirection.Output);
            
            var jobs = await dataAccessor
                        .QueryAsync<Job>
                            (
                            "GetJobs",
                            parameters,
                            commandType: CommandType.StoredProcedure
                            );
            return jobs;
        }


        public async Task<IEnumerable<Job>> InvokeAnonymous(string param1 = null, string param2 = null)
        {
            return await DataAccessorAsync(async dataAccessor =>
            {
                var parameters = new DynamicParameters();
                if (param1 != null)
                {
                    parameters.Add("param1", param1);
                }
                if (param2 != null)
                {
                    parameters.Add("param2", param2);
                }
                parameters.Add("Emsg", dbType: DbType.String, direction: ParameterDirection.Output);

                var jobs = await dataAccessor
                            .QueryAsync<Job>
                                (
                                "GetJobs",
                                parameters,
                                commandType: CommandType.StoredProcedure
                                );
                return jobs;
            });
        }
    }

    public class Job
    {
        public int JobId { get; set; }
    }
}