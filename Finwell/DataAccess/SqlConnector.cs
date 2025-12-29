using Dapper;
using FinwellLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FinwellLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        // ===== AUTHENTICATE USER (LOGIN) =====
        public userModel AuthenticateUser(string username, string password)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@Username", username);
                p.Add("@Password", password);

                var user = connection.Query<userModel>(
                    "dbo.spUsers_Authenticate",
                    p,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                return user;
            }
        }

        // ===== CREATE USER (REGISTRATION) =====
        public userModel CreateUser(userModel user)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@Username", user.UserName);  // ✅ Match stored proc parameter
                p.Add("@Password", user.Password);
                p.Add("@UserId", 0, DbType.Int32, ParameterDirection.Output);

                connection.Execute(
                    "dbo.spUsers_Insert2",
                    p,
                    commandType: CommandType.StoredProcedure);

                user.UserId = p.Get<int>("@UserId");
                return user;
            }
        }

        // ===== GET USER BY ID =====
        public userModel GetUserById(int id)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", id);

                return connection.Query<userModel>(
                    "dbo.spUsers_GetById",
                    p,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        // ===== GET USER BY USERNAME =====
        public userModel GetUserByUsername(string username)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@Username", username);

                return connection.Query<userModel>(
                    "dbo.spUsers_GetByUsername",
                    p,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        // ===== UPDATE USER =====
        public void UpdateUser(userModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", model.UserId);
                p.Add("@Username", model.UserName);
                p.Add("@Password", model.Password);

                connection.Execute(
                    "dbo.spUsers_Update",
                    p,
                    commandType: CommandType.StoredProcedure);
            }
        }
        // ===== SCENARIO METHODS =====
        public List<scenarioModel> GetAllScenarios()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                try
                {
                    var scenarios = connection.Query<scenarioModel>(
                        "dbo.spScenarios_GetAll",
                        commandType: CommandType.StoredProcedure).ToList();

                    return scenarios;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error loading scenarios: {ex.Message}", ex);
                }
            }
        }

        public scenarioModel GetScenarioById(int id)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@ScenarioId", id);

                    var scenario = connection.Query<scenarioModel>(
                        "dbo.spScenarios_GetById",
                        p,
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return scenario;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error loading scenario: {ex.Message}", ex);
                }
            }
        }
        /// <summary>
        /// Creates a new monthly decision record
        /// </summary>
        public int CreateMonthlyDecision(montlyDecisionModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@SimulationId", model.SimulationId);
                p.Add("@MonthNumber", model.MonthNumber);
                p.Add("@DebtPayment", model.DebtPayment);
                p.Add("@InvestmentAmount", model.InvestmentAmount);
                p.Add("@RemainingBalance", model.RemainingBalance);
                p.Add("@LivingExpensesPaid", model.LivingExpensesPaid);
                p.Add("@InterestAccrued", model.InterestAccrued);
                p.Add("@DebtBalanceAfter", model.DebtBalanceAfter);
                p.Add("@InvestmentValueAfter", model.InvestmentValueAfter);
                p.Add("@NetWorth", model.NetWorth);

                int decisionId = connection.QuerySingle<int>(
                    "spMonthlyDecision_Insert",
                    p,
                    commandType: CommandType.StoredProcedure
                );

                model.DecisionId = decisionId;
                return decisionId;
            }
        }
        /// <summary>
        /// Gets a specific monthly decision by simulation and month
        /// </summary>
        public montlyDecisionModel GetMonthlyDecision(int simulationId, int monthNumber)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@SimulationId", simulationId);
                p.Add("@MonthNumber", monthNumber);

                return connection.QueryFirstOrDefault<montlyDecisionModel>(
                    "spMonthlyDecision_GetBySimulationAndMonth",
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        /// <summary>
        /// Gets the latest decision for a simulation
        /// </summary>
        public montlyDecisionModel GetLatestDecision(int simulationId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@SimulationId", simulationId);

                return connection.QueryFirstOrDefault<montlyDecisionModel>(
                    "spMonthlyDecision_GetLatest",
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public int CreateSimulation(simulationModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", model.UserId);
                p.Add("@ScenarioId", model.ScenarioId);

                int simulationId = connection.QuerySingle<int>(
                    "spSimulation_Insert",
                    p,
                    commandType: CommandType.StoredProcedure
                );

                model.SimulationId = simulationId;
                return simulationId;
            }
        }
        /// <summary>
        /// Gets an active (non-completed) simulation for a user and scenario
        /// </summary>
        public simulationModel GetActiveSimulation(int userId, int scenarioId)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
                {
                    var p = new DynamicParameters();
                    p.Add("@UserId", userId);
                    p.Add("@ScenarioId", scenarioId);

                    var result = connection.QueryFirstOrDefault<simulationModel>(
                        "spSimulation_GetActive",
                        p,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting active simulation: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets all decisions for a simulation (already exists, just making sure)
        /// </summary>
        public List<montlyDecisionModel> GetAllDecisions(int simulationId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@SimulationId", simulationId);

                return connection.Query<montlyDecisionModel>(
                    "spMonthlyDecision_GetAllBySimulation",
                    p,
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        /// <summary>
        /// Updates simulation status and current month
        /// </summary>
        public void UpdateSimulationStatus(int simulationId, string status, int currentMonth)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
                {
                    var p = new DynamicParameters();
                    p.Add("@SimulationId", simulationId);
                    p.Add("@Status", status);
                    p.Add("@CurrentMonth", currentMonth);

                    connection.Execute(
                        "spSimulation_UpdateStatus",
                        p,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating simulation status: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets simulation result by simulation ID
        /// </summary>
        public simulationResultModel GetSimulationResult(int simulationId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@SimulationId", simulationId);

                return connection.QueryFirstOrDefault<simulationResultModel>(
                    "spSimulationResult_Get",
                    p,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        /// <summary>
        /// Creates or updates a simulation result record
        /// </summary>
        public int CreateSimulationResult(simulationResultModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@SimulationId", model.SimulationId);
                p.Add("@TotalMonths", model.TotalMonths);
                p.Add("@FinalNetWorth", model.FinalNetWorth);
                p.Add("@TotalInterestPaid", model.TotalInterestPaid);
                p.Add("@TotalInvestmentGains", model.TotalInvestmentGains);
                p.Add("@EfficiencyScore", model.EfficiencyScore);
                p.Add("@TotalDebtPaid", model.TotalDebtPaid);
                p.Add("@UserId", model.UserId);

                int resultId = connection.QuerySingle<int>(
                    "spSimulationResult_Insert",
                    p,
                    commandType: CommandType.StoredProcedure
                );

                model.ResultId = resultId;
                return resultId;
            }
        }

        /// <summary>
        /// Gets overall leaderboard across all scenarios
        /// </summary>
        public List<LeaderboardEntryModel> GetOverallLeaderboard(int topN = 100)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@TopN", topN);

                var entries = connection.Query<LeaderboardEntryModel>(
                    "spLeaderboard_GetOverall",
                    p,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Assign ranks
                for (int i = 0; i < entries.Count; i++)
                {
                    entries[i].Rank = i + 1;
                }

                return entries;
            }
        }

        /// <summary>
        /// Gets leaderboard for a specific scenario
        /// </summary>
        public List<LeaderboardEntryModel> GetScenarioLeaderboard(int scenarioId, int topN = 100)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("FinWell_2025")))
            {
                var p = new DynamicParameters();
                p.Add("@ScenarioId", scenarioId);
                p.Add("@TopN", topN);

                var entries = connection.Query<LeaderboardEntryModel>(
                    "spLeaderboard_GetByScenario",
                    p,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                // Assign ranks
                for (int i = 0; i < entries.Count; i++)
                {
                    entries[i].Rank = i + 1;
                }

                return entries;
            }
        }

    }
}