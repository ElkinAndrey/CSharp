import React, { useState } from "react";
import ReactDOM from "react-dom/client";
import axios from "axios";

const getUsers = async () => {
  const response = await axios.get(`api/Users`);
  return response.data;
};

const useFetching = (callback) => {
  const [isLoading, setIsLoading] = useState(false); // Равно true, если данные пока, что получаются
  const [error, setError] = useState(""); // Если возникнет ошибка

  // Получение данных
  const fetching = async (...args) => {
    try {
      setIsLoading(true);
      await callback(...args); // Начать получение данных
    } catch (e) {
      setError(e.message);
    } finally {
      setIsLoading(false);
    }
  };

  return [fetching, isLoading, error];
};

function App() {
  let [users, setUsers] = useState([]);

  const [fetchUsers, isUsersLoading, usersError] = useFetching(async () => {
    const data = await getUsers();
    setUsers(data);
  });

  return (
    <div>
      <button onClick={fetchUsers}>Получить пользователей</button>
      {users === null || users.length === 0 ? (
        <div>Списка нет</div>
      ) : (
        <div>{users.map((user) =>(
          <div key={user.name}>
            {user.name}
          </div>
        ))}</div>
      )}
    </div>
  );
}

export default App;
