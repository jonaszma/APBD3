using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameCorrect(firstName) || IsLastNameCorrect(lastName))
            {
                return false;
            }

            if (IsEmailCorrect(email))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (!IsAgeCorrect(age)) return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (IsClientTypeVeryImportantClient(client))
            {
                user.HasCreditLimit = false;
            }
            else if (IsClientTypeImportantClient(client))
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (IsCreditLimitCorrect(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static bool IsCreditLimitCorrect(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private static bool IsClientTypeImportantClient(Client client)
        {
            return client.Type == "ImportantClient";
        }

        private static bool IsClientTypeVeryImportantClient(Client client)
        {
            return client.Type == "VeryImportantClient";
        }

        private static bool IsAgeCorrect(int age)
        {
            if (age < 21)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmailCorrect(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        private static bool IsLastNameCorrect(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstNameCorrect(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}
