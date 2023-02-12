import React, { useEffect, useState } from 'react';
import {Button, Modal, ModalFooter} from 'react-bootstrap';
import AtividadeForm from './AtividadeForm';
import AtividadesLista from './AtividadesLista';
import api from '../../api/atividade';
import TitlePage from '../../components/TitlePage';

export default function App() {
  const [showAtividadeModal, setShowAtividadeModal] = useState(false);
  const [smShowConfirmeModal, setSmShowConfirmeModal] = useState(false);
  
  const [atividades, setAtividades] = useState([]) 
  const [atividade, setAtividade] = useState({id:0})  

  const handleAtividadeModal = () => setShowAtividadeModal(!showAtividadeModal);

  const handleConfirmeModal = (id) => {
    if(id !== 0 && id !== undefined){
      const atividade = atividades.filter((atividade) => atividade.id === id);
      setAtividade(atividade[0]);
    }
    else{
      setAtividade({id:0});
    }
    setSmShowConfirmeModal(!smShowConfirmeModal);
  } 
  
  const pegarTodasAtividades = async () =>{
    const response = await api.get('atividade')
    return response.data;
  }

  useEffect(() => {
    const getAtividades = async () =>{
      const todasAtividades = await pegarTodasAtividades();
      if(todasAtividades) setAtividades(todasAtividades);
    };
    getAtividades();
  }, [])

  const addAtividade = async (ativ) => { 
    const response = await api.post('atividade', ativ);
    setAtividades([ ...atividades, response.data]
      );
    handleAtividadeModal();
  };

  const novaAtividade = () => {
    setAtividade({id:0})
    handleAtividadeModal();
  }

  const cancelarAtividade = () => {
    setAtividade({id:0})
    handleAtividadeModal();
  };

  const atualizarAtividade = async (ativ) =>{
    const response = await api.put(`atividade/${ativ.id}`, ativ);
    const {id} = response.data;
    setAtividades(
      atividades.map((item) => (item.id === id ? response.data : item))
    );
    setAtividade({id:0});
    handleAtividadeModal();
 };

  const deletarAtividade = async (id) => {
    handleConfirmeModal(0);
    if(await api.delete(`atividade/${id}`)){
      const atividadeFiltradas = atividades.filter((atividade) => atividade.id !== id);
      setAtividades([...atividadeFiltradas]);
    }
  };

  const pegarAtividade = (id) => {
    const atividade = atividades.filter((atividade) => atividade.id === id);
    setAtividade(atividade[0]);
    handleAtividadeModal();
  };

  return (
    <>
      <TitlePage
        title={'Atividade ' + (atividade.id !== 0 ? atividade.id : '')}
      >
        <Button variant="outline-secondary" onClick={novaAtividade}>
          <i className='fa fa-plus'></i>
        </Button>
      </TitlePage>
        
      <AtividadesLista
        atividades={atividades}
        handleConfirmeModal={handleConfirmeModal}
        pegarAtividade={pegarAtividade}
      />

      <Modal show={showAtividadeModal} onHide={handleAtividadeModal}>
        <Modal.Header closeButton>
          <Modal.Title>
            Atividade {atividade.id !== 0 ? atividade.id : ''}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AtividadeForm
          addAtividade={addAtividade}
          cancelarAtividade={cancelarAtividade}
          atualizarAtividade={atualizarAtividade}
          atividadeSelecionada={atividade}
          atividades={atividades}          
          />
        </Modal.Body>        
      </Modal>

      <Modal 
        size='sm'
        show={smShowConfirmeModal}
        onHide={handleConfirmeModal}
        >
        <Modal.Header closeButton>
          <Modal.Title>
            Excluindo Atividade{' '}
            {atividade.id !== 0 ? atividade.id : ''}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Tem certeza que deseja Excluir esta atividade {atividade.id}
        </Modal.Body>   
        <ModalFooter className='d-flex justify-content-between'>
            <button
             className='btn btn-outline-success me-2'
             onClick={() => deletarAtividade(atividade.id)}
            >
              <i className='fa fa-check me-2'></i>
              Sim
            </button>
            <button 
              className='btn btn-danger me-2' 
              onClick={() => handleConfirmeModal(0)}
            >
              <i className='fa fa-check me-2'></i>
              Não
            </button>
        </ModalFooter>     
      </Modal>
    </>    
  );
}
