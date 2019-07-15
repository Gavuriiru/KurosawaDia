﻿using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.DAO
{
    public class StatusDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public void AdicionarAtualizarStatus(List<Status> statuses)
        {
            RemoverTabela();
            conexao.Open();
            string sql = "insert into Status (status_jogo, status_tipo) values (@status, @tipo)";
            for (int i = 0; i < statuses.Count; i++)
            {
                SqliteCommand cmd = new SqliteCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@status", statuses[i].status_jogo);
                cmd.Parameters.AddWithValue("@tipo", statuses[i].status_tipo);
                cmd.ExecuteNonQuery();
            }

            conexao.Close();
        }

        public void RemoverTabela()
        {
            string sql = "drop TABLE if EXISTS Status;";
            conexao.Open();
            SqliteCommand cmd = new SqliteCommand(sql, conexao);
            cmd.ExecuteNonQuery();
            cmd = new SqliteCommand(DB.sqlCriacao[3], conexao);
            cmd.ExecuteNonQuery();
            conexao.Close();
        }


        public Tuple <bool, List<Status>> CarregarStatus()
        {
            const string sql = "select * from Status;";
            conexao.Open();
            SqliteCommand cmd = new SqliteCommand(sql, conexao);
            SqliteDataReader rs = cmd.ExecuteReader();
            bool estado = false;
            List<Status> retorno = new List<Status>();
            while (rs.Read())
            {
                Status temp = new Status(Convert.ToUInt32(rs["status_id"]) ,(string)rs["status_jogo"], (Status.TiposDeStatus)Convert.ToInt32(rs["status_tipo"]));
                retorno.Add(temp);
                estado = true;
            }
            conexao.Close();
            return Tuple.Create(estado, retorno);
        }
    }
}