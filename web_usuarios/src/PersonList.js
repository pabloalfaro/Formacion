import React from 'react';
//import axios from 'axios';
import './person.css';

import API from './api';

export default class PersonList extends React.Component {
  state = {
    persons: [],
    id: ''
  }

  handleChange = event => {
    this.setState({ id: event.target.value });
  }

  handleSubmit = event => {
    event.preventDefault();

    API.get(`users/${this.state.id}`)
      .then(res => {
        var persona;
        if (this.state.id == ""){
            persona = res.data;
        } else {
            persona = [res.data];
        }       
        this.setState({ persons: persona });
        console.log(res);
        console.log(res.data);
      })
  }
  /*
  componentDidMount() {
    API.get(`users`)
      .then(res => {
        const persons = res.data;
        this.setState({ persons });
      })
  }
  */

  render() {
    return (
        <div>
        <form onSubmit={this.handleSubmit}>
          <label>
            Person ID:
            <input type="text" name="id" onChange={this.handleChange} />
          </label>
          <button type="submit">Buscar</button>
        </form>

      <table>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Usuario</th>
            <th>Email</th>
            <th>Ciudad</th>
            <th>Web</th>
            <th>Compañía</th>
          </tr>
          <tbody>
          { this.state.persons.map(person =>
          <tr>
            <td>{person.id}</td>
            <td>{person.name}</td>
            <td>{person.username}</td>
            <td>{person.email}</td>
            <td>{person.address.city}</td>
            <td>{person.website}</td>
            <td>{person.company.name}</td>
          </tr>
          )}    
          </tbody>
      </table>
      </div>
    )
  }
}