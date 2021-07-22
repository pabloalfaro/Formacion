import React from 'react';
//import axios from 'axios';
import './person.css';

import apiEmpresas from './apiEmpresas';

export default class PersonList extends React.Component {
  state = {
    empresas: [],
    id: ''
  }

  handleChange = event => {
    this.setState({ id: event.target.value });
  }

  handleSubmit = event => {
    event.preventDefault();

    apiEmpresas.get(`/${this.state.id}`)
      .then(res => {
        var empresa;
        empresa = res.data;     
        this.setState({ empresas: empresa });
        console.log(res);
        console.log(res.data);
      })
  }
  /*
  componentDidMount() {
    API.get(`users`)
      .then(res => {
        const empresas = res.data;
        this.setState({ empresas });
      })
  }
  */

  render() {
    return (
        <div>
        <form onSubmit={this.handleSubmit}>
          <label>
            Buscador: 
            <input type="text" name="id" onChange={this.handleChange} />
          </label>
          <button type="submit">Buscar</button>
        </form>

      <table>
          <tr>
            <th>Nombre Sociedad</th>
            <th>CIF</th>
            <th>Coincidencia Por</th>
            <th>Datos Registrales</th>
          </tr>
          <tbody>
          { this.state.empresas.map(empresa =>
          <tr>
            <td>{empresa.nombreSociedad}</td>
            <td>{empresa.cif}</td>
            <td>{empresa.coincidenciaPor}</td>
            <td>{empresa.datosRegistrales}</td>
          </tr>
          )}    
          </tbody>
      </table>
      </div>
    )
  }
}