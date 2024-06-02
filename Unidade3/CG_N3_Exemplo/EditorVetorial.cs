using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace gcgcg
{
    internal class EditorVetorial : Objeto
    {
        List<Poligono> poligonos = new List<Poligono>();
        int idxPoligonoAtual = -1;

        public EditorVetorial(Objeto _paiRef, ref char _rotulo) : base(_paiRef, ref _rotulo)
        {
        }

        public void AdicionarPoligono(Poligono _poligono)
        {
            poligonos.Add(_poligono);
            idxPoligonoAtual = poligonos.Count - 1;
        }

        public void AdicionarPontoPoligonoAtual(Ponto4D _ponto)
        {
            poligonos[idxPoligonoAtual].PontosAdicionar(_ponto);
        }

        public void MoverVerticeMaisProximoPoligono(Ponto4D pontoMouse)
        {
            double menorDistancia = 0;
            int idxPontoMenorDistancia = -1;

            for (int idxPonto = 0; idxPonto < poligonos[idxPoligonoAtual].PontosListaTamanho; ++idxPonto)
            {
                double distancia = Matematica.Distancia(pontoMouse, poligonos[idxPoligonoAtual].PontosId(idxPonto));

                if (distancia < menorDistancia || idxPonto == 0)
                {
                    idxPontoMenorDistancia = idxPonto;
                    menorDistancia = distancia;
                }
            }

            if (idxPontoMenorDistancia != -1)
                poligonos[idxPoligonoAtual].PontosAlterar(pontoMouse, idxPontoMenorDistancia);
        }

        public void RemoverVerticeMaisProximoPoligono(Ponto4D pontoMouse)
        {
            double menorDistancia = 0;
            int idxPontoMenorDistancia = -1;

            for (int idxPonto = 0; idxPonto < poligonos[idxPoligonoAtual].PontosListaTamanho; ++idxPonto)
            {
                double distancia = Matematica.Distancia(pontoMouse, poligonos[idxPoligonoAtual].PontosId(idxPonto));

                if (distancia < menorDistancia || idxPonto == 0)
                {
                    idxPontoMenorDistancia = idxPonto;
                    menorDistancia = distancia;
                }
            }

            if (idxPontoMenorDistancia != -1)
                poligonos[idxPoligonoAtual].PontosRemover(idxPontoMenorDistancia);
        }

        public void RemoverPoligono()
        {
            if (poligonos.Count == 0)
                return;

            base.FilhoRemover(poligonos[idxPoligonoAtual]);
            poligonos.RemoveAt(idxPoligonoAtual);

            base.ObjetoAtualizar();
        }

        public void AlteraAberturaFechamentoPoligonoAtual()
        {
            poligonos[idxPoligonoAtual].PrimitivaTipo = (poligonos[idxPoligonoAtual].PrimitivaTipo == PrimitiveType.LineStrip) ? PrimitiveType.LineLoop : PrimitiveType.LineStrip;
        }

        public void SelecionaPoligono(Ponto4D pontoMouse)
        {
            // Se o número total de cruzamentos é ímpar, o ponto está dentro do polígono.

            int qtdPontosCruzados = 0;

            foreach (Poligono objPoligono in poligonos)
            {
                if (!objPoligono.Bbox().Dentro(pontoMouse))
                    continue;

                if (Matematica.ScanLine(pontoMouse, objPoligono.PontosId(0), objPoligono.PontosId(objPoligono.PontosListaTamanho - 1)))
                    ++qtdPontosCruzados;

                for (int idxPonto = 0; idxPonto < objPoligono.PontosListaTamanho - 1; ++idxPonto)
                {
                    Ponto4D ponto1 = objPoligono.PontosId(idxPonto);
                    Ponto4D ponto2 = objPoligono.PontosId(idxPonto + 1);

                    if (Matematica.ScanLine(pontoMouse, ponto1, ponto2))
                        ++qtdPontosCruzados;
                }


                if (qtdPontosCruzados % 2 != 0)
                {
                    int i = 0;
                    //objetoSelecionado = 
                }
            }
        }
    }
}
