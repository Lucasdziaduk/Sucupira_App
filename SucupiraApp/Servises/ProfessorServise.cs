using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SucupiraApp.Models;
using SucupiraApp.Database;

namespace SucupiraApp.Servises
{
    public class ProfessorService
    {
        private readonly DatabaseService _db;

        public ProfessorService()
        {
            _db = new DatabaseService();
        }

        public async Task CadastrarProfessorAsync(Professor professor)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var sql = @"INSERT INTO Professor 
                (Nome, Email, SenhaHash, LattesUrl, Ativo, NivelPermissao, RegistroAcoes) 
                VALUES (@Nome, @Email, @SenhaHash, @LattesUrl, @Ativo, @NivelPermissao, @RegistroAcoes)";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Nome", professor.Nome);
            cmd.Parameters.AddWithValue("Email", professor.Email);
            cmd.Parameters.AddWithValue("SenhaHash", professor.SenhaHash);
            cmd.Parameters.AddWithValue("LattesUrl", professor.LattesUrl);
            cmd.Parameters.AddWithValue("Ativo", professor.Ativo);
            cmd.Parameters.AddWithValue("NivelPermissao", professor.NivelPermissao.ToString());
            cmd.Parameters.AddWithValue("RegistroAcoes", professor.RegistroAcoes.ToArray());

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Professor>> ListarProfessoresAsync()
        {
            var professores = new List<Professor>();

            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var sql = "SELECT * FROM Professor";
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var professor = new Professor
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    SenhaHash = reader.GetString(reader.GetOrdinal("SenhaHash")),
                    LattesUrl = reader.GetString(reader.GetOrdinal("LattesUrl")),
                    Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                    NivelPermissao = Enum.Parse<NivelPermissao>(reader.GetString(reader.GetOrdinal("NivelPermissao"))),
                    RegistroAcoes = reader.IsDBNull(reader.GetOrdinal("RegistroAcoes"))
                        ? new List<string>()
                        : reader.GetFieldValue<string[]>(reader.GetOrdinal("RegistroAcoes")).ToList()
                };

                professores.Add(professor);
            }

            return professores;
        }

        public async Task<Professor?> BuscarProfessorPorIdAsync(int id)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var sql = "SELECT * FROM Professor WHERE Id = @Id";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Professor
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    SenhaHash = reader.GetString(reader.GetOrdinal("SenhaHash")),
                    LattesUrl = reader.GetString(reader.GetOrdinal("LattesUrl")),
                    Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                    NivelPermissao = Enum.Parse<NivelPermissao>(reader.GetString(reader.GetOrdinal("NivelPermissao"))),
                    RegistroAcoes = reader.IsDBNull(reader.GetOrdinal("RegistroAcoes"))
                        ? new List<string>()
                        : reader.GetFieldValue<string[]>(reader.GetOrdinal("RegistroAcoes")).ToList()
                };
            }

            return null;
        }

        public async Task AtualizarProfessorAsync(Professor professor)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var sql = @"UPDATE Professor SET 
                        Nome = @Nome, Email = @Email, SenhaHash = @SenhaHash, 
                        LattesUrl = @LattesUrl, Ativo = @Ativo, 
                        NivelPermissao = @NivelPermissao, RegistroAcoes = @RegistroAcoes
                        WHERE Id = @Id";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", professor.Id);
            cmd.Parameters.AddWithValue("Nome", professor.Nome);
            cmd.Parameters.AddWithValue("Email", professor.Email);
            cmd.Parameters.AddWithValue("SenhaHash", professor.SenhaHash);
            cmd.Parameters.AddWithValue("LattesUrl", professor.LattesUrl);
            cmd.Parameters.AddWithValue("Ativo", professor.Ativo);
            cmd.Parameters.AddWithValue("NivelPermissao", professor.NivelPermissao.ToString());
            cmd.Parameters.AddWithValue("RegistroAcoes", professor.RegistroAcoes.ToArray());

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ExcluirProfessorAsync(int id)
        {
            using var conn = _db.GetConnection();
            await conn.OpenAsync();

            var sql = "DELETE FROM Professor WHERE Id = @Id";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", id);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
