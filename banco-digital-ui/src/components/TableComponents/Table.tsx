import { useState } from 'react';
import { MdDeleteForever, MdEdit, MdRefresh } from 'react-icons/md';
import { DeleteConfirmationModal } from './DeleteConfirmationModal';


interface TableProps<T extends object, K extends keyof T> {
    tableHeaders: string[],
    tableData: T[],
    variavelId: K, 
    editT: (id: T[K]) => void, 
    deleteT: (id: T[K]) => void,
    loading: boolean
}

export const Table = <T extends object, K extends keyof T>({ tableHeaders, tableData, variavelId, editT, deleteT, loading }: TableProps<T, K>) => {
    const [deleteId, setDeleteId] = useState<T[K] | null>(null);

    function handleEdit(id: T[K]) {
        editT(id);
    }

    function handleDelete(id: T[K]) {
        setDeleteId(id);
    }

    function confirmDelete() {
        if (deleteId !== null) {
            deleteT(deleteId);
            setDeleteId(null);
        }
    }

    return (
        <>
            <table className="table-auto w-full border-collapse">
                <thead>
                    <tr className="bg-gray-200">
                        {tableHeaders.map((headerItem, index) => (
                            <th key={index} className="px-4 py-2">{headerItem}</th>
                        ))}
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                {loading ? (
                    <tr>
                        <td colSpan={tableHeaders.length + 2} className="text-center py-4">
                            <div className="flex items-center justify-center">
                                <MdRefresh className="animate-spin h-8 w-8 text-gray-500" />
                                <span className="ml-2">Carregando...</span>
                            </div>
                        </td>
                    </tr>
                    ) : (
                    tableData.map((rowData, rowIndex) => (
                        <tr key={rowIndex} className={rowIndex % 2 === 1 ? "bg-gray-100" : ""}>
                            {Object.keys(rowData).map((key, colIndex) => (
                                <td key={colIndex} className="border px-4 py-2">{String(rowData[key as keyof T])}</td>
                            ))}
                            <td className="border px-4 py-2">
                                <MdEdit onClick={() => handleEdit(rowData[variavelId])} />
                            </td>
                            <td className="border px-4 py-2">
                                <MdDeleteForever onClick={() => handleDelete(rowData[variavelId])} />
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
        </>
    );
};
