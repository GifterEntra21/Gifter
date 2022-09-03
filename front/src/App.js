import logo from './logo.svg';
import './App.css';
import axios from 'axios';


const url = 'https://localhost:7008/users/authenticate'
var a 
function Authenticate()
{
    axios.post(url,{
      username:"test",
      password:"test"
    }).then(response => {
      const data = response.data
      a = JSON.stringify(data) 
    }).catch(error => console.log(error))
}

Authenticate()
function App() {
  return (
    <div className="App">
      <p>{a}</p>
      <p>vasco</p>
    </div>
  );
}
Authenticate()
export default App;
