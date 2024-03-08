import { useState } from 'react';
import { MdDeleteForever, MdEdit, MdRefresh } from 'react-icons/md';
import { DeleteConfirmationModal } from './DeleteConfirmationModal';
import { Pagination } from './Pagination';
import { CreateItem } from './CreateItem';

interface TableProps<T extends object, K extends keyof T> {
    tableHeaders: string[],
    tableData: T[],
    variableId: K, 
    editT: (id: T[K]) => void, 
    deleteT: (id: T[K]) => void,
    loading: boolean,
    buttonText: string,
    handleCreateButtonClick: () => void,    
}

export const Table = <T extends object, K extends keyof T>({ tableHeaders, tableData, variableId: variavelId, editT, deleteT, loading, buttonText, handleCreateButtonClick }: TableProps<T, K>) => {
    const [deleteId, setDeleteId] = useState<T[K] | null>(null);
    const [currentPage, setCurrentPage] = useState(1);

    const itemsPerPage = 10;
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = tableData.slice(indexOfFirstItem, indexOfLastItem);

    const handleEdit = (id: T[K]) => {
        editT(id);
    }

    const handleDelete = (id: T[K]) => {
        setDeleteId(id);
    }

    const confirmDelete = () => {
        if (deleteId !== null) {
            deleteT(deleteId);
            setDeleteId(null);
        }
    }

    return (
            <div className="flex flex-col space-y-4">
                <CreateItem text={buttonText} handleClick={handleCreateButtonClick}/>
                <table className="min-w-full divide-y divide-gray-200">
                    <thead className="bg-gray-50">
                        <tr>
                            {tableHeaders.map((headerItem, index) => (
                                <th key={index} className="px-6 py-4 text-left text-md font-semibold text-black-900 uppercase">{headerItem}</th>
                            ))}
                            <th className="px-6 py-4"></th>
                            <th className="px-6 py-4"></th>
                        </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                    {loading ? (
                        <tr>
                            <td colSpan={tableHeaders.length + 2} className="text-center py-8">
                                <div className="flex items-center justify-center">
                                    <MdRefresh className="animate-spin h-12 w-12 text-gray-500" />
                                    <span className="ml-4 text-xl font-semibold">Carregando...</span>
                                </div>
                            </td>
                        </tr>
                        ) : (
                        currentItems.map((rowData, rowIndex) => (
                            <tr key={rowIndex} className={rowIndex % 2 === 0 ? "bg-gray-50" : "bg-white"}>
                                {Object.keys(rowData).map((key, colIndex) => (
                                    <td key={colIndex} className="px-6 py-4 whitespace-nowrap text-md text-gray-900">{String(rowData[key as keyof T])}</td>
                                ))}
                                <td className="px-6 py-4 whitespace-nowrap text-right text-md">
                                    <MdEdit size={20} onClick={() => handleEdit(rowData[variavelId])} className="text-blue-600 cursor-pointer hover:scale-125" />
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-right text-md">
                                    <MdDeleteForever size={20} onClick={() => handleDelete(rowData[variavelId])} className="text-red-600 cursor-pointer hover:scale-125" />
                                </td>
                            </tr>
                        ))
                    )}
                    </tbody>
                </table>
                <DeleteConfirmationModal
                    isOpen={deleteId !== null}
                    onClose={() => setDeleteId(null)}
                    onConfirm={confirmDelete}
                />
                <Pagination currentPage={currentPage} totalPages={Math.ceil(tableData.length / itemsPerPage)} onPageChange={setCurrentPage} />
            </div>
        );
};
