import { SubmitHandler, useForm } from "react-hook-form";
import { axiosInstance } from "../api/axios";
import { Client } from "../types/Client";
import { Dispatch, SetStateAction, useEffect } from "react";


type ClientInputs = {
    name: string;
    birthDate: Date | string;
    cpf: string;
    address: string;
    clientType: number | string;
}

interface ClientPageProps{
    setIsModalVisible(visible: boolean): void;
    addNewClient(newClient: ClientInputs | Client): void;
    selectedClientId: number | null;
    setSelectedClientId: Dispatch<SetStateAction<number | null>>;
    clients: Client[];
    setClients: Dispatch<SetStateAction<Client[]>>;
}

export const ClientForm = ({ setIsModalVisible, addNewClient, selectedClientId, setSelectedClientId, clients, setClients }: ClientPageProps) => {
    const { register, handleSubmit, reset, setValue } = useForm<ClientInputs>();

    const mapClientType = (clientType: string | number): string => {
        switch (clientType) {
            case "Common":
                return "0";
            case "Super":
                return "1";
            case "Premium":
                return "2";
            default:
                return "0";
        }
    };
    

    useEffect(() => {
        if (selectedClientId !== null) {
            const selectedClient = clients.find(client => client.id === selectedClientId);
            if (selectedClient) {
                setValue("name", selectedClient.name);
                const birthDate = new Date(selectedClient.birthDate);
                setValue("birthDate", birthDate.toISOString().split('T')[0]);
                setValue("cpf", selectedClient.cpf);
                setValue("address", selectedClient.address);
                const mappedClientType = mapClientType(selectedClient.clientType);
                setValue("clientType", mappedClientType);
            }
        } else {
            reset();
        }
    }, [selectedClientId, clients, reset, setValue]);

    const onFormSubmit: SubmitHandler<ClientInputs> = async (data) => {
        console.log(data)
        if (selectedClientId){
            axiosInstance.put(`/api/v1/clientes/${selectedClientId}`, data)
            .then((res) =>{
                setClients(clients.map(client =>
                    client.id === selectedClientId ? res.data : client
                    ));
                    setIsModalVisible(false);
                    reset();
                    setSelectedClientId(null);
                })
                .catch((error) => {
                    console.log(error);
                })
            }
        else {
            data.clientType = Number(data.clientType);
            await axiosInstance.post('/api/v1/clientes', data)
            .then((res) =>{
                addNewClient(res.data)
                setIsModalVisible(false);
                reset();
                })
            .catch((error) =>{
                console.log(error);
                })
            }
        };
    
    return (
        <form className="w-full max-w-md mx-auto rounded px-8 pb-4 mt-4" onSubmit={handleSubmit(onFormSubmit)}>
            <label htmlFor="name" className="block text-gray-700 font-bold mb-2">Nome<span className="text-red-600">*</span></label>
            <input type="text" id="name" {...register("name")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>
            
            <label htmlFor="birthDate" className="block text-gray-700 font-bold mb-2">Data de Nascimento<span className="text-red-600">*</span></label>
            <input type="date" id="birthDate" {...register("birthDate")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="cpf" className="block text-gray-700 font-bold mb-2">CPF</label>
            <input type="text" id="cpf" {...register("cpf")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="address" className="block text-gray-700 font-bold mb-2">Endereço</label>
            <input type="text" id="address" {...register("address")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="clientType" className="block text-gray-700 font-bold mb-2">Tipo do Cliente<span className="text-red-600">*</span></label>
            <select id="clientType" {...register("clientType")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true">
                <option value="0">Comum</option>
                <option value="1">Super</option>
                <option value="2">Premium</option>
            </select>

            <button type="submit" className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded">Enviar</button>
        </form>
    );
};
