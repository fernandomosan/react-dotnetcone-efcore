import React, { useState } from 'react'
import { Button, Form, InputGroup } from 'react-bootstrap'
import { useHistory } from 'react-router-dom'
import TitlePage from '../../components/TitlePage'

const clientes = [
    {
        id: 1,
        nome: 'Microsoft',
        responsavel: 'Otto',
        contato: '1037887283',
        situacao: 'Ativo'
    },
    {
        id: 2,
        nome: 'Amazon',
        responsavel: 'Pitanga',
        contato: '1037887283',
        situacao: 'Desativado'
    },
    {
        id: 3,
        nome: 'Google',
        responsavel: 'Pedro',
        contato: '7898793876',
        situacao: 'Em Análise'
    },
    {
        id: 4,
        nome: 'Facebook',
        responsavel: 'Marcia',
        contato: '876656523',
        situacao: 'Ativo'
    },
    {
        id: 5,
        nome: 'Twitter',
        responsavel: 'Jack',
        contato: '782378872',
        situacao: 'Ativo'
    }
]

export default function ClienteLista() {
    
    const history = useHistory();   
    const [termoBusca, setTermoBusca] = useState('');

    const handleInputChage = (e) => {
        setTermoBusca(e.target.value);
    };

    const clientesFiltrados = clientes.filter((cliente) => {
        return Object.values(cliente)
                    .join(' ')
                    .toLowerCase()
                    .includes(termoBusca.toLowerCase())
    });

    const novoCliente = () => {
        history.push('/cliente/detalhe');
    }

    return (
        <>
            <TitlePage title='Cliente Lista'>
                <Button variant='outline-secondary' onClick={novoCliente}>
                    <i className='fas fa-plus me-2'></i>
                    Novo Cliente
                </Button>
            </TitlePage>
                
            <InputGroup className="mb-3 mt-3">          
            <InputGroup.Text>
                Pesquisar:                
                </InputGroup.Text>
                <Form.Control onChange={handleInputChage} placeholder='Buscar por cliente'/>
            </InputGroup>
            <table className='table table-striped table-hover'>
                <thead className='table-dark mt-3'>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nome</th>
                        <th scope="col">Responsavel</th>
                        <th scope="col">Contato</th>
                        <th scope="col">Situação</th>
                        <th scope="col">Opções</th>
                    </tr>
                </thead>
                <tbody>
                    {clientesFiltrados.map((cliente) =>(
                    <tr key={cliente.id}>
                        <td>{cliente.id}</td>
                        <td>{cliente.nome}</td>
                        <td>{cliente.responsavel}</td>
                        <td>{cliente.contato}</td>
                        <td>{cliente.situacao}</td>
                        <td>
                            <div>
                                <button 
                                    className='btn btn-sm btn-outline-primary me-2' 
                                    onClick={() => (history.push(
                                        `/cliente/detalhe/${cliente.id}`
                                    ))}>
                                    <i className='fas fa-user-edit me-2'></i>                                    
                                    Editar
                                </button>
                                <button className='btn btn-sm btn-outline-danger me-2'>
                                    <i className='fas fa-user-times me-2'></i>
                                    Desativar
                                </button>
                            </div>
                        </td>
                    </tr>
                    ))}
                </tbody>
            </table>
        </>
    )
}
