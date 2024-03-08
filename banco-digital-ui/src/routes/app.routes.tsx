import { Route, Routes } from 'react-router-dom'
import { Navbar } from '../components/Navbar'
import { Homepage } from '../pages/Homepage'
import { Clients } from '../pages/Clients'
import { Cards } from '../pages/Cards'
import { Accounts } from '../pages/Accounts'
import { Policies } from '../pages/Policies'

export const AppRoutes = () => {
  return (
    <>
        <Navbar />
        <Routes>
            <Route element={<Homepage />} path=""/>
            <Route element={<Clients />} path="/clientes"/>
            <Route element={<Accounts />} path="/contas"/>
            <Route element={<Cards />} path="/cartoes"/>
            <Route element={<Policies />} path="/apolices"/>
        </Routes>
    </>
  )
}
