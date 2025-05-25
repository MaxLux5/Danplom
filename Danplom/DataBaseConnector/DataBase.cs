using Danplom.Model;
using Dapper;
using MySqlConnector;
using System.Windows;

namespace Danplom.DataBaseConnector
{
    /// <summary>
    /// Singleton-класс для обращения в базу данных.
    /// </summary>
    public class DataBase
    {
        private static DataBase _instance;
        private const string _connectionString = "Server=localhost; Username=root; Password=24681357; Database=Danplom";


        private DataBase() { }


        public static DataBase GetInstance()
        {
            return _instance ??= new DataBase();
        }

        // TODO Убрать лишний код
        #region
        //public IEnumerable<ExecutorViewModel>? GetExecutorCollection()
        //{
        //    using (MySqlConnection connection = new MySqlConnection(_connectionString))
        //    {
        //        try
        //        {
        //            var queryString =
        //            """
        //            select id as Id,
        //            executor_name as Name
        //            from executors
        //            """;

        //            return connection.Query<ExecutorViewModel>(queryString);
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Не удалось загрузить данные.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return null;
        //        }
        //    }
        //}
        //public IEnumerable<MaterialViewModel>? GetMaterialCollection()
        //{
        //    using (MySqlConnection connection = new MySqlConnection(_connectionString))
        //    {
        //        try
        //        {
        //            var queryString =
        //            """
        //            select id as Id,
        //            material_name as Name,
        //            stock_quantity as StockQuantity,
        //            measurement_unit as MeasurementUnit
        //            from materials
        //            """;

        //            return connection.Query<MaterialViewModel>(queryString);
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Не удалось загрузить данные.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return null;
        //        }
        //    }
        //}
        ///// <summary>
        ///// Изменяет в базе данных исполнителя и статус заданной заявки.
        ///// В случае ошибки при попытке изменения данных появится сообщение об ошибке.
        ///// </summary>
        ///// <param name="updatedRequest">Обновленная заявка.</param>
        ///// <returns>Возвращает true, если изменение данных прошло успешно, иначе возвращает false.</returns>
        //public bool ChangeRequest(RequestViewModel updatedRequest)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(_connectionString))
        //    {
        //        try
        //        {
        //            var queryString =
        //            $"""
        //            update requests
        //            set requests.executor_id = '{updatedRequest?.Executor.Id}',
        //            requests.request_status = '{(int)updatedRequest?.Status}'
        //            where requests.id = '{updatedRequest?.Id}'
        //            """;

        //            connection.Execute(queryString);
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Не удалось обновить заявку.", "Ошибка сохранения!", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return false;
        //        }

        //        return true;
        //    }
        //}

        ///// <summary>
        ///// Изменяет в базе данных количество запасов заданного материала.
        ///// В случае ошибки при попытке изменения данных появится сообщение об ошибке.
        ///// </summary>
        ///// <param name="changedMaterial">Изменяемый материал.</param>
        ///// <param name="changingQuantity">Число, меняющее количество StockQuantity у материала.</param>
        ///// <returns>Возвращает true, если изменение данных прошло успешно, иначе возвращает false.</returns>
        //public bool ChangeMaterialStockQuantity(MaterialViewModel changedMaterial, int changingQuantity)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(_connectionString))
        //    {
        //        try
        //        {
        //            if (changedMaterial.StockQuantity + changingQuantity < 0) return false;

        //            var queryString =
        //            $"""
        //            update materials
        //            set materials.stock_quantity = '{changedMaterial.StockQuantity + changingQuantity}'
        //            where materials.id = '{changedMaterial.Id}'
        //            """;

        //            connection.Execute(queryString);
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Не удалось обновить данные материала.", "Ошибка сохранения!", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return false;
        //        }

        //        return true;
        //    }
        //}
        #endregion
        /// <summary>
        /// Проверяет есть ли пользователь в базе данных.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Возвращает UserDto, если пользователь есть в базе данных, иначе возвращает null.</returns>
        public UserDto? Login(string login, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    $"""
                    select users.id as Id,
                    users.user_name as Name,
                    users.user_role as Role,
                    users.login as Login,
                    users.password as Password
                    from users
                    where users.login = '{login}'
                    and users.password = '{password}'
                    """;

                    var user = connection.QuerySingle<UserDto>(queryString);
                    if (user is not null) return user;
                    else return CallErrorMessage();
                }
                catch (Exception)
                {
                    return CallErrorMessage();
                }
            }

            UserDto? CallErrorMessage()
            {
                MessageBox.Show("Такого пользователя не существует.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public IEnumerable<RequestDto>? GetRequestCollection()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    """
                    select requests.id as Id,
                    details.id as detailId,
                    details.detail_name as detailName,
                    requests.required_quantity as RequiredQuantity,
                    requests.time_to_complete as TimeToComplete,
                    users.id as userId,
                    users.user_name as userName,
                    users.user_role as role,
                    users.login as login,
                    users.password as password,
                    requests.request_status as status
                    from requests
                    left join details
                    ON requests.detail_id = details.id
                    left join users
                    ON requests.user_id = users.id
                    """;

                    return connection.Query<RequestDto>(queryString);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось загрузить данные.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }
        /// <summary>
        /// Добавляет в базу данных заданную заявку без Id. Но в базе данных Id устанавливается самостоятельно
        /// по умолчанию. В случае ошибки при попытке добавления данных появится сообщение об ошибке.
        /// </summary>
        /// <param name="newRequest">Новая заявка.</param>
        /// <returns>Возвращает true, если добавление данных прошло успешно, иначе возвращает false.</returns>
        public bool AddNewRequest(RequestDto newRequest)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    $"""
                    insert into requests(required_quantity, time_to_complete,
                    request_status, detail_id, user_id)
                    values('{newRequest.RequiredQuantity}', '{newRequest.TimeToComplete}',
                    '{(int)newRequest.Status}', '{newRequest.Detail.Id}', '{newRequest.User.Id}')
                    """;

                    connection.Execute(queryString);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось добавить новую заявку.", "Ошибка сохранения!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }
        }
        /// <summary>
        /// Удаляет из базы данных заданную заявку. В случае ошибки при выполнении появится сообщение об ошибке.
        /// </summary>
        /// <param name="deletedRequest">Удаляемая заявка.</param>
        /// <returns>Возвращает true, если удаление данных прошло успешно, иначе возвращает false.</returns>
        public bool DeleteRequest(RequestDto deletedRequest)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    $"""
                    delete from requests
                    where requests.id = '{deletedRequest.Id}'
                    """;

                    connection.Execute(queryString);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить данные.", "Ошибка удаления!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }
        }

        public IEnumerable<DetailDto>? GetDetailCollection()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    """
                    select details.id as Id,
                    details.detail_name as Name
                    from details
                    """;

                    return connection.Query<DetailDto>(queryString);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось загрузить данные.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }
        public IEnumerable<UserDto>? GetExecutorCollection()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    """
                    select users.id as Id,
                    users.user_name as Name,
                    users.user_role as Role,
                    users.login as Login,
                    users.password as Password
                    from users
                    where users.user_role = 1
                    """;

                    return connection.Query<UserDto>(queryString);
                }
                catch (Exception)
                {
                    throw;
                    MessageBox.Show("Не удалось загрузить данные.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }

        public IEnumerable<RequestDto>? GetCurrentExecutorRequestCollection(UserDto currentUser)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    $"""
                    select requests.id as Id,
                    details.id as detailId,
                    details.detail_name as detailName,
                    requests.required_quantity as RequiredQuantity,
                    requests.time_to_complete as TimeToComplete,
                    users.id as userId,
                    users.user_name as userName,
                    users.user_role as role,
                    users.login as login,
                    users.password as password,
                    requests.request_status as status
                    from requests
                    left join details
                    ON requests.detail_id = details.id
                    left join users
                    ON requests.user_id = users.id
                    where users.id = '{currentUser.Id}'
                    """;

                    return connection.Query<RequestDto>(queryString);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось загрузить данные.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }

        public bool ChangeRequest(RequestDto updatedRequest)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    $"""
                    update requests
                    set requests.request_status = '{(int)updatedRequest?.Status}'
                    where requests.id = '{updatedRequest?.Id}'
                    """;

                    connection.Execute(queryString);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось обновить заявку.", "Ошибка сохранения!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }
        }
    }
}
