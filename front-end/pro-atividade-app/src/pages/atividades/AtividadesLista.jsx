import React from 'react'
import AtividadeItem from './AtividadeItem';

export default function AtividadesLista(props) {
  return (
    <div className="mt-3">
        {props.atividades.map((ativ) => (
            <AtividadeItem
                key={ativ.id}
                ativ={ativ}
                handleConfirmeModal={props.handleConfirmeModal}
                pegarAtividade={props.pegarAtividade}
            />
        ))}
    </div>
  )
}
