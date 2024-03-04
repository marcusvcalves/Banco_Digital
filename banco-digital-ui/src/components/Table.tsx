import { MdDeleteForever, MdEdit } from "react-icons/md";

interface TableProps<T extends object, K extends keyof T> {
    tableHeaders: string[],
    tableData: T[],
    variavelId: K, 
    editT: (id: T[K]) => void, 
    deleteT: (id: T[K]) => void 
}

export const Table = <T extends object, K extends keyof T>({ tableHeaders, tableData, variavelId, editT, deleteT }: TableProps<T, K>) => {
    function handleEdit(id: T[K]) {
        editT(id);
    }

    function handleDelete(id: T[K]) {
        deleteT(id);
    }

    return (
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
                {tableData.map((rowData, rowIndex) => (
                    <tr key={rowIndex} className={rowIndex % 2 === 1 ? "bg-gray-100" : ""}>
                        {Object.keys(rowData).map((key, colIndex) => (
                            <td key={colIndex} className="border px-4 py-2">{String(rowData[key as keyof T])}</td>
                        ))}
                        <td className="border px-4 py-2">
                            <MdEdit onClick={() => handleEdit(rowData[variavelId])}/>
                        </td>
                        <td className="border px-4 py-2">
                            <MdDeleteForever onClick={() => handleDelete(rowData[variavelId])}/>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};