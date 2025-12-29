using FinwellLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary.DataAccess
{
    public interface IDataConnection
    {  // ===== USER METHODS =====
        /// <summary>
        /// Creates a new user in the database with hashed password
        /// </summary>
        userModel CreateUser(userModel user); 
        //userModel GetUserByUsername(string username);

        /// <summary>
        /// Authenticates user credentials and returns user if valid
        /// </summary>
        userModel AuthenticateUser(string username, string password);
        // Add this method to your IDataConnection interface
        int CreateSimulation(simulationModel model);

        /// <summary>
        /// Gets user information by ID
        /// </summary>
        userModel GetUserById(int id);

        /// <summary>
        /// Updates user profile information
        /// </summary>
        void UpdateUser(userModel model);

        // ===== SCENARIO METHODS =====
        /// <summary>
        /// Gets all available scenarios for selection
        /// </summary>
        List<scenarioModel> GetAllScenarios();

        /// <summary>
        /// Gets a specific scenario by ID
        /// </summary>
        scenarioModel GetScenarioById(int id);
        int CreateMonthlyDecision(montlyDecisionModel model);
        montlyDecisionModel GetMonthlyDecision(int simulationId, int monthNumber);
        montlyDecisionModel GetLatestDecision(int simulationId);
        //List<montlyDecisionModel> GetAllDecisions(int simulationId);
        // Add these methods to your IDataConnection interface
        simulationModel GetActiveSimulation(int userId, int scenarioId);
        List<montlyDecisionModel> GetAllDecisions(int simulationId);
        void UpdateSimulationStatus(int simulationId, string status, int currentMonth);

        simulationResultModel GetSimulationResult(int simulationId);
        int CreateSimulationResult(simulationResultModel model);
        // Add these methods to IDataConnection interface
        // Add these methods to IDataConnection interface
        List<LeaderboardEntryModel> GetOverallLeaderboard(int topN = 100);
        List<LeaderboardEntryModel> GetScenarioLeaderboard(int scenarioId, int topN = 100);

        // Simulation management methods
        //simulationModel GetActiveSimulation(int userId, int scenarioId);
        //void UpdateSimulationStatus(int simulationId, string status, int currentMonth);
    }
}
