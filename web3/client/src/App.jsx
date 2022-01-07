import { Navbar, Welcome, Footer, Services, Transactions } from './Components';

function App() {

  return (
    <div className="min-h-screen">
      <h1 className="gradient-bg-welcome">
        <Navbar />
        <Welcome />
      </h1>
      <Services />
      <Transactions />
      <Footer />
    </div>
  )
}

export default App
