import { SubmitHandler, useForm } from "react-hook-form";
import { axiosInstance } from "../api/axios";

type ClientInputs = {
    nome: string;
    dataNascimento: string;
    cpf: string;
    endereco: string;
    tipoCliente: "Comum" | "Super" | "Premium";
}

interface ClientPageProps{
    setIsModalVisible(visible: boolean): void;
    addNewClient(newClient: ClientInputs): void;
}

export const ClientForm = ({ setIsModalVisible, addNewClient }: ClientPageProps) => {
    const { register, handleSubmit, reset } = useForm<ClientInputs>();

    const onFormSubmit: SubmitHandler<ClientInputs> = async (data) => {
        await axiosInstance.post('/api/v1/clientes', data)
        .then((res) =>{
            console.log(res.data);
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
            <input type="text" id="nome" {...register("nome")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>
            
            <label htmlFor="dataNascimento" className="block text-gray-700 font-bold mb-2">Data de Nascimento<span className="text-red-600">*</span></label>
            <input type="date" id="dataNascimento" {...register("dataNascimento")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="cpf" className="block text-gray-700 font-bold mb-2">CPF</label>
            <input type="text" id="cpf" {...register("cpf")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="endereco" className="block text-gray-700 font-bold mb-2">Endereço</label>
            <input type="text" id="endereco" {...register("endereco")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="tipoCliente" className="block text-gray-700 font-bold mb-2">Tipo de Cliente</label>
            <select id="tipoCliente" {...register("tipoCliente")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4">
                <option value="Comum">Comum</option>
                <option value="Super">Super</option>
                <option value="Premium">Premium</option>
            </select>

            <button type="submit" className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded">Enviar</button>
        </form>
    );
};
