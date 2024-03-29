import { useEffect, useState } from "react"
import { axiosInstance } from "../api/axios";
import { Table } from "../components/TableComponents/Table";
import { Modal, Tabs } from "antd";
import { CardForm } from "../components/CardForm";
import { Card } from "../types/Card";


export const Cards = () => {
  const [cards, setCards] = useState<Card[]>([]);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [isModalVisible, setIsModalVisible] = useState<boolean>(false);

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

  const tableHeaders = ['ID', 'Número', 'Tipo do Cartão', 'Status do Cartão', 'Número da Conta'];
  const tableData = cards.map(card => ({
    "id": card.id,
    "numero": card.number,
    "tipoCartao": card.cardType === "Debit" ? "Débito" : card.cardType === "Credit" ? "Crédito" : "Normal",
    "cartaoAtivo": card.activeCard ? "Ativo" : "Inativo",
    "contaId": card.account.number
  }));

  const editCard = (id: number): void => {
    setIsModalVisible(true);
    setSelectedCardId(id);
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

  const handleCreateButtonClick = (): void => {
    setIsModalVisible(true);
  };

  const handleModalClose = (): void => {
    setIsModalVisible(false);
    setSelectedCardId(null);
  };

  const addCard = (newCard: Card): void => {
    setCards([...cards, newCard])
  }

  const tabsItems = [
    {
      key: "1",
      label: "Lista de Cartões",
      children: <Table
                  tableHeaders={tableHeaders}
                  tableData={tableData}
                  variableId="id"
                  editT={editCard}
                  deleteT={deleteCard}
                  loading={loading}
                  buttonText="Cadastrar Cartão"
                  handleCreateButtonClick={handleCreateButtonClick}
                />
    },
    {
      key: "2",
      label: "Mudar Senha do Cartão"
    }
  ]



  return (
        <div className="flex justify-center pt-10">
        <div className="text-center">
          <h2 className="text-2xl font-bold mb-4">Cartões</h2>
          <Tabs defaultActiveKey="1" items={tabsItems}>
          </Tabs>
        </div>
        <Modal
          open={isModalVisible}
          onCancel={handleModalClose}
          footer={null}
        >
          <CardForm
            setIsModalVisible={setIsModalVisible}
            addCard={addCard}
            selectedCardId={selectedCardId}
            setSelectedCardId={setSelectedCardId}
            cards={cards}
            setCards={setCards}
            />
        </Modal>
      </div>
  );
};