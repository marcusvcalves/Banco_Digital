import { SubmitHandler, useForm } from "react-hook-form";
import { axiosInstance } from "../api/axios";
import { Client } from "../types/Client";


type ClientInputs = {
    name: string;
    birthDate: Date;
    cpf: string;
    address: string;
    clientType: number;
}

interface ClientPageProps{
    setIsModalVisible(visible: boolean): void;
    addNewClient(newClient: ClientInputs | Client): void;
}

export const ClientForm = ({ setIsModalVisible, addNewClient }: ClientPageProps) => {
    const { register, handleSubmit, reset } = useForm<ClientInputs>();

    const onFormSubmit: SubmitHandler<ClientInputs> = async (data) => {
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
    };
    
    return (
        <form className="w-full max-w-md mx-auto rounded px-8 pb-4 mt-4" onSubmit={handleSubmit(onFormSubmit)}>
            <label htmlFor="nome" className="block text-gray-700 font-bold mb-2">Nome<span className="text-red-600">*</span></label>
            <input type="text" id="nome" {...register("name")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>
            
            <label htmlFor="dataNascimento" className="block text-gray-700 font-bold mb-2">Data de Nascimento<span className="text-red-600">*</span></label>
            <input type="date" id="dataNascimento" {...register("birthDate")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="cpf" className="block text-gray-700 font-bold mb-2">CPF</label>
            <input type="text" id="cpf" {...register("cpf")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="endereco" className="block text-gray-700 font-bold mb-2">Endereço</label>
            <input type="text" id="endereco" {...register("address")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="tipoCliente" className="block text-gray-700 font-bold mb-2">Tipo do Cliente<span className="text-red-600">*</span></label>
            <select id="tipoCliente" {...register("clientType")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true">
                <option value="0">Comum</option>
                <option value="1">Super</option>
                <option value="2">Premium</option>
            </select>

            <button type="submit" className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded">Enviar</button>
        </form>
    );
};
