import { useEffect, useState } from "react"
import { axiosInstance } from "../api/axios";
import { Table } from "../components/TableComponents/Table";
import { Tabs } from "antd";
import TabPane from "antd/es/tabs/TabPane";

interface CardProps {
  id: number,
  statusCartao: number,
  numero: string,
  contaId: number
}

export const Cartoes = () => {
  const [cards, setCards] = useState<CardProps[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const getCards = (): void => {
    setLoading(true);
    axiosInstance
      .get("/api/v1/cartoes")
      .then((res) => {
        setCards(res.data.$values);
        setLoading(false);
      })
      .catch((error) => {
        console.log(`Erro ao buscar cartões: ${error}`);
      })
  };

  useEffect(() => {
    getCards();
  }, []);

  const tableHeaders = ['ID', 'Status do Cartão', 'Número', 'Conta ID'];
  const tableData = cards.map(card => ({
    "id": card.id,
    "statusCartao": card.statusCartao,
    "numero": card.numero,
    "contaId": card.contaId
  }));

  const editCard = (id: number): void => {
    console.log(`editar cartão ${id}`)
  };

  const deleteCard = (id: number): void => {
    axiosInstance
      .delete(`/api/v1/cartoes/${id}`)
      .then(() => {
        setCards(cards.filter(card => card.id !== id));
      })
      .catch((error) => {
        console.log(`Erro ao deletar cartão: ${error}`);
      });
  };


  return (
        <div className="flex justify-center pt-10">
        <div className="text-center">
          <h2 className="text-2xl font-bold mb-4">Cartões</h2>
          <Tabs defaultActiveKey="1">
            <TabPane tab="Lista de Cartões" key="1">
              <Table
                tableHeaders={tableHeaders}
                tableData={tableData}
                variavelId="id"
                editT={editCard}
                deleteT={deleteCard}
                loading={loading}
              />
            </TabPane>
            <TabPane tab="Mudar Senha do Cartão" key="2">
              {/* Conteúdo da outra aba */}
            </TabPane>
          </Tabs>
        </div>
      </div>
  );
};