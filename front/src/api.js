import axios from 'axios';

const api = axios.create({
  baseURL: "https://localhost:7008/",
});

export default api;