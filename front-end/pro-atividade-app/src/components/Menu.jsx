import {Navbar, Container, Nav, NavDropdown} from 'react-bootstrap';
import { NavLink } from 'react-router-dom';

export default function Menu() {
  return (
    <Navbar bg="dark" expand="lg" variant='dark'>
      <Container>
        <Navbar.Brand 
            as={NavLink} 
            to='/'
        >
            Ativy
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link 
                activeClassName='active'
                as={NavLink} 
                to='/cliente/lista'
            >
                Cliente
            </Nav.Link>
            <Nav.Link 
                activeClassName='active'
                as={NavLink} 
                to='/atividades/lista'>
                Atividade
            </Nav.Link>
          </Nav>
            <Nav>
                <NavDropdown 
                    align='end' 
                    title="Fernando" 
                    id="basic-nav-dropdown"
                    >
                    <NavDropdown.Item href="#action/3.1">
                        Perfil
                    </NavDropdown.Item>
                    <NavDropdown.Item href="#action/3.2">
                        Configurações
                    </NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item href="#action/3.4">
                        Separated link
                    </NavDropdown.Item>
                </NavDropdown>
            </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}
