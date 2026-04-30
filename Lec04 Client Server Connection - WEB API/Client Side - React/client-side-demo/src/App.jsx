import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import heroImg from './assets/hero.png'
import './App.css'

function App() {

  let apiUrl = 'https://localhost:7103/api/StudentsRW/';

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
          result.map(st => console.log(st.name));
          console.log('result[0].Name=', result[0].name);
        },
        (error) => {
          console.log("err post=", error);
        });
  }

  const btnGetByID = () => {

    fetch(apiUrl + "2", {
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
          console.log('result.Name=', result.name);
        },
        (error) => {
          console.log("err post=", error);
        });
  }

  const btnPost = () => {

    const data2Send = { //pay attention case sensitive!!!! should be exactly as the prop in C#!
      Id: 0,
      Name: 'nir',
      Grade: 77
    };

    fetch(apiUrl, {
      method: 'POST',
      body: JSON.stringify(data2Send),
      headers: new Headers({
        'Content-type': 'application/json; charset=UTF-8', //very important to add the 'charset=UTF-8'!!!!
        'Accept': 'application/json; charset=UTF-8',
      })
    })
      .then(response => {
        console.log('response=', response);
        return response.json()
      })
      .then(
        (result) => {
          console.log("fetch POST= ", result);
          console.log(result.grade);
        },
        (error) => {
          console.log("err post=", error);
        });
  }

  const btnPut = () => {

    const data2Send = { //pay attention case sensitive!!!! should be exactly as the prop in C#!
      Id: 2,
      Name: 'nir',
      Grade: 77
    };

    fetch(apiUrl + "2", {
      method: 'PUT',
      body: JSON.stringify(data2Send),
      headers: new Headers({
        'Content-type': 'application/json; charset=UTF-8', //very important to add the 'charset=UTF-8'!!!!
        'Accept': 'application/json; charset=UTF-8',
      })
    })
      .then(response => {
        console.log('response=', response);
        console.log(response.status);
        console.log(response.status == 204);        
      },
        (error) => {
          console.log("err post=", error);
        });
  }

  const btnDelete = () => {
    let id2Del = 2;
    
    fetch(apiUrl + id2Del, {
      method: 'DELETE',
      headers: new Headers({
        'Content-type': 'application/json; charset=UTF-8', //very important to add the 'charset=UTF-8'!!!!
        'Accept': 'application/json; charset=UTF-8',
      })
    })
      .then(response => {
        console.log('response=', response);
        console.log(response.status);
        console.log(response.status == 204);        
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

        <button className="counter" onClick={btnGetALL}>Get All</button>
        <button className="counter" onClick={btnGetByID}>Get By ID</button>
        <button className="counter" onClick={btnPost}>Post</button>
        <button className="counter" onClick={btnPut}>Put</button>
        <button className="counter" onClick={btnDelete}>Delete</button>
      </section>




    </>
  )
}

export default App
