import { Route, Routes } from 'react-router-dom'
import { Navbar } from '../components/Navbar'
import { Homepage } from '../pages/Homepage'
import { Clientes } from '../pages/Clientes'
import { Cartoes } from '../pages/Cartoes'
import { Contas } from '../pages/Contas'
import { Apolices } from '../pages/Apolices'

export const AppRoutes = () => {
  return (
    <>
        <Navbar />
        <Routes>
            <Route element={<Homepage />} path=""/>
            <Route element={<Clientes />} path="/clientes"/>
            <Route element={<Contas />} path="/contas"/>
            <Route element={<Cartoes />} path="/cartoes"/>
            <Route element={<Apolices />} path="/apolices"/>
        </Routes>
    </>
  )
}
