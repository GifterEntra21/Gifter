import logo from './logo.svg';
import './App.css';
import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:7008/'
//axios.defaults.headers.common = {'Authorization': `bearer ${token}`}
var Token

function GetPictures(){
  axios.post({
    baseURL: 'https://localhost:7008/users/authentication',
    headers:{},
    data:{
      username:"test",
      password:"test"
    }
  }).then(response => console.log(response)).catch(error => console.log(error))


}


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}
GetPictures()
export default App;
