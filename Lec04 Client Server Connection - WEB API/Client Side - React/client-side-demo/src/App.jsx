import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import heroImg from './assets/hero.png'
import './App.css'

function App() {

  apiUrl = 'https://localhost:7103/api/StudentsRW/';

  const btnGetALL = () => {

    fetch(apiUrl, {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json; charset=UTF-8',
        'Accept': 'application/json; charset=UTF-8',
      })
    })
      .then(response => {
        console.log('response=', response);
        console.log('response.status', response.status);
        console.log('response.ok', response.ok);
        return response.json()
      })
      .then(
        (result) => {
          console.log("fetch btnFetchGetStudents= ", result);
          result.map(st => console.log(st.Name));
          console.log('result[0].Name=', result[0].Name);
        },
        (error) => {
          console.log("err post=", error);
        });

  }

  return (
    <>
      <section id="center">
        <div className="hero">
          <img src={heroImg} className="base" width="170" height="179" alt="" />
          <img src={reactLogo} className="framework" alt="React logo" />
          <img src={viteLogo} className="vite" alt="Vite logo" />
        </div>
        <div>

        </div>

        <button className="counter"
          onClick={btnGetALL}
        >Get All</button>
      </section>




    </>
  )
}

export default App
