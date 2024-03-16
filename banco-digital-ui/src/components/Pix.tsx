import { SubmitHandler, useForm } from "react-hook-form";
import { axiosInstance } from "../api/axios";



type PixInputs = {
    senderAccountId: number,
    receiverAccountId: number,
    amount: number
}

interface PixProps{
    updateAccountBalance: (accountId: number, newBalance: number) => void;
}

export const Pix = ({ updateAccountBalance }:PixProps) => {
    const { register, handleSubmit, reset } = useForm<PixInputs>();

    const onFormSubmit: SubmitHandler<PixInputs> = async (data) =>{
        await axiosInstance.post(`/api/v1/contas/${data.senderAccountId}/${data.receiverAccountId}/${data.amount}`)
        .then((res) => {
            updateAccountBalance(res.data.$values[0].id, res.data.$values[0].balance);
            updateAccountBalance(res.data.$values[1].id, res.data.$values[1].balance);
            reset();
        })
        .catch((error) => {
            console.log(error);
        })

    }

    return (
    <div>
        <form className="flex flex-col" onSubmit={handleSubmit(onFormSubmit)}>
            <label htmlFor="senderAccountId">ID da Conta Enviante</label>
            <input type="text" id="senderAccountId" {...register("senderAccountId")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label>ID da Conta Recebedora</label>
            <input type="text" id="receiverAccountId" {...register("receiverAccountId")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label>Quantia</label>
            <input type="text" id="amount" {...register("amount")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <button type="submit" className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded">Enviar</button>
        </form>
    </div>
    )
}
