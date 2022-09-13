using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI.ACESSO.DADOS
{
    public class CONEXAO
    {
        private static string conexao = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        public static string StringConexao
        {
            get { return conexao; }
        }

        //CRIAR CONEXAO.............................................................
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(conexao);
        }

        //PARAMETROS QUE VAI PARA BANCO............................................
        //CRIA UMA CLASSE DE COLEÇÃO
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        //LIMPAR CLASSE DE COLECAO.................................................
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        //ADCIONAR VALORES E PARAMETRO............................................
        //NOME = @BANCO DE DADOS
        //VALOR = VALOR QUE ESTA PASSANDO
        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            //todas as vezes que for chamado um valor do parametro a colecao de parametro ira levar ate banco........
            sqlParameterCollection.Add(nomeParametro, valorParametro);
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        //PARTE DE PESISTENCIA = (INSERIR, ALTERAR, DELETAR)
        public object ExecutarManipulacao(CommandType commandType, string nomeStoreProcedureOuTextoSql)
        {
            try
            {
                //OBS DO PARAMETRO DO METODO (ExecutarManipulacao).
                // commandType = ENUMERADOR--(CONJUNTO DE OPCAO QUE NAO MUDA - EX.SEXO (F/M)) TEM QUE PASSAR EM UM OBJETO, OU UM TEXTO.
                // nomeStoreProcedureOuTextoSql = POSSO NOME DA PROCEDURE LA DO BANCO OU TEXTO QUALQUER

                // 1- CRIAR UMA CONEXAO
                //criar um objeto de conexao.....
                SqlConnection sqlConnection = CriarConexao();

                // 2-ABRIR CONEXAO
                sqlConnection.Open();

                // 3-CRIAR COMANDO PARA LEVAR ATE OS DADOS ATE BANCO - (EX -SEDEX (CAIXA)) TRAZENDO O RETORNO DO PROCESSO - (RESPOSTA)
                SqlCommand sqlComando = sqlConnection.CreateCommand();

                // 4-FUNCAO PARA COLOCAR AS INFORMAÇÃO DENTRO DO COMANDO QUE VAI TRAFEGAR NO BANCO 
                //sqlComando.CommandType = CommandType.StoredProcedure;
                //sqlComando.CommandType = CommandType.Text;
                sqlComando.CommandType = commandType; // - traz os texto que exite no retorno da procedure do banco. 


                //fazendo esse comando elemino esse  sqlComando.CommandType =     //sqlComando.CommandType = CommandType.StoredProcedure;
                //sqlComando.CommandType = CommandType.Text;
                //é uma funcao fazendo as duas coisa ao msm tempo,, ou seja esta trazendo falor do returno de uma procedure ou de um texto la do banco de dados.

                sqlComando.CommandText = nomeStoreProcedureOuTextoSql; // passo o nome do metodo - INSERIR, DELETAR, ALTERAR..
                sqlComando.CommandTimeout = 7200; // 7200  em segundos == (2horas), para prteger tempo de banco aberto por segurança........

                // 5 - ADIIONAR OS PARAMETRO NO COMANDO

                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    //corre a lista de valore em cada atributo do banco um por um ate acabar os mesmo
                    //CONSTRUTOR
                    sqlComando.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                    //executa linha alinha
                    //sqlParameter = ParameterName = nome do parametro do banco(@NOME)
                    //sqlParameterCollection = sqlParameter.Value =  valor do parametro do banco(CLAUDEMIR)
                }

                // 6 - EXECUTA TODOS OS COMANDO E TRAZ A INFORMACAO OU ID    DA PROCEDURE QUE ESTA NO BANCO = (SELECT @@IDENTITY AS RETORNO)
                return sqlComando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //tratamento que joga o erro pra outro projeto ou seja pra outra camada de tratamento...
                //nesse caso camada regra negocios
            }
        }

        //METODO PARA CONSULTAR REGISTRO DO BANCO ATE O APLICATIVO
        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoreProcedureOuTextoSql)
        {
            try
            {
                //OBS DO PARAMETRO DO METODO (ExecutarManipulacao).
                // commandType = ENUMERADOR--(CONJUNTO DE OPCAO QUE NAO MUDA - EX.SEXO (F/M)) TEM QUE PASSAR EM UM OBJETO, OU UM TEXTO.
                // nomeStoreProcedureOuTextoSql = POSSO NOME DA PROCEDURE LA DO BANCO OU TEXTO QUALQUER

                // 1- CRIAR UMA CONEXAO
                //criar um objeto de conexao.....
                SqlConnection sqlConnection = CriarConexao();

                // 2-ABRIR CONEXAO
                sqlConnection.Open();

                // 3-CRIAR COMANDO PARA LEVAR ATE OS DADOS ATE BANCO - (EX -SEDEX (CAIXA)) TRAZENDO O RETORNO DO PROCESSO - (RESPOSTA)
                SqlCommand sqlComando = sqlConnection.CreateCommand();

                // 4-FUNCAO PARA COLOCAR AS INFORMAÇÃO DENTRO DO COMANDO QUE VAI TRAFEGAR NO BANCO 
                //sqlComando.CommandType = CommandType.StoredProcedure;
                //sqlComando.CommandType = CommandType.Text;
                sqlComando.CommandType = commandType; // - traz os texto que exite no retorno da procedure do banco. 


                //fazendo esse comando elemino esse  sqlComando.CommandType =     //sqlComando.CommandType = CommandType.StoredProcedure;
                //sqlComando.CommandType = CommandType.Text;
                //é uma funcao fazendo as duas coisa ao msm tempo,, ou seja esta trazendo falor do returno de uma procedure ou de um texto la do banco de dados.

                sqlComando.CommandText = nomeStoreProcedureOuTextoSql; // passo o nome do metodo - INSERIR, DELETAR, ALTERAR..
                sqlComando.CommandTimeout = 7200; // 7200  em segundos == (2horas), para prteger tempo de banco aberto por segurança........

                // 5 - ADIIONAR OS PARAMETRO NO COMANDO

                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    //corre a lista de valore em cada atributo do banco um por um ate acabar os mesmo
                    //CONSTRUTOR
                    sqlComando.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                    //executa linha alinha
                    //sqlParameter = ParameterName = nome do parametro do banco(@NOME)
                    //sqlParameterCollection = sqlParameter.Value =  valor do parametro do banco(CLAUDEMIR)
                }

                //criar o adaptador
                //adaptador é um traduto que sistema e banco falem a mesma lingua....
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlComando);

                //tabela de dados vazia.
                DataTable dataTable = new DataTable();

                //comando para ir ate banco e trazer os dados e o adapitador e preencher os dados da tabela

                sqlDataAdapter.Fill(dataTable);
                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //tratamento que joga o erro pra outro projeto ou seja pra outra camada de tratamento...
                //nesse caso camada regra negocios
            }
        }
    }
}
