import { useEffect, useState } from "react"
import { axiosInstance } from "../api/axios";
import { Table } from "../components/TableComponents/Table";
import { format } from "date-fns";
import { Modal, Tabs } from "antd";
import { PolicyForm } from "../components/PolicyForm";
import { Policy } from "../types/Policy";


export const Policies = () => {
  const [policies, setPolicies] = useState<Policy[]>([]);
  const [selectedPolicyId, setSelectedPolicyId] = useState<number | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [isModalVisible, setIsModalVisible] = useState(false);

  const getPolicies = (): void => {
    setLoading(true);
    axiosInstance
      .get("/api/v1/apolices")
      .then((res) => {
        setPolicies(res.data.$values);
        setLoading(false);
      })
      .catch((error) => {
        console.log(`Erro ao buscar apólices: ${error}`)
      })
  };

  useEffect(() => {
    getPolicies();
  }, []);

  const tableHeaders = ['ID', 'Número', 'Valor', 'Data de Contratação', 'Descrição de Acionamento', 'Número do Cartão'];
  const tableData = policies.map(policy => ({
    "id": policy.id,
    "numero": policy.number,
    "valor": policy.value,
    "dataContratacao": format(new Date(policy.hiringDate), 'dd/MM/yyyy'),
    "descricaoAcionamento": policy.triggeringDescription,
    "creditCardNumber": policy.creditCard.number
  }));

  const editPolicy = (id: number): void => {
    setIsModalVisible(true);
    setSelectedPolicyId(id);
  };

  const deletePolicy = (id: number): void => {
    axiosInstance
      .delete(`/api/v1/apolices/${id}`)
      .then(() => {
        setPolicies(policies.filter(policy => policy.id !== id));
      })
      .catch((error) => {
        console.log(`Erro ao deletar apólice: ${error}`);
      });
  };

  const handleCreateButtonClick = (): void => {
    setIsModalVisible(true);
  };

  const handleModalClose = (): void => {
    setIsModalVisible(false);
    setSelectedPolicyId(null);
  };

  const addPolicy = (newPolicy: Policy): void => {
    setPolicies([...policies, newPolicy]);
  };

  const tabsItems = [
    {
      key: "1",
      label: "Lista de Apólices",
      children: <Table
                  tableHeaders={tableHeaders}
                  tableData={tableData}
                  variableId="id"
                  editT={editPolicy}
                  deleteT={deletePolicy}
                  loading={loading}
                  buttonText="Cadastrar Apólice"
                  handleCreateButtonClick={handleCreateButtonClick}
                />
    },
    {
      key: "2",
      label: "Gerar Apólice Eletrônica"

    }
  ]

  return (
        <div className="flex justify-center pt-10">
        <div className="text-center">
          <h2 className="text-2xl font-bold mb-4">Apólices</h2>
          <Tabs defaultActiveKey="1" items={tabsItems}>
          </Tabs>
        </div>
        <Modal
          open={isModalVisible}
          onCancel={handleModalClose}
          footer={null}
        >
          <PolicyForm
            setIsModalVisible={setIsModalVisible} 
            addPolicy={addPolicy}
            selectedPolicyId={selectedPolicyId}
            setSelectedPolicyId={setSelectedPolicyId}
            policies={policies}
            setPolicies={setPolicies}
          />
        </Modal>
      </div>
  );
};