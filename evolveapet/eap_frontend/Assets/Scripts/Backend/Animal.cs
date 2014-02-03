using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace EvolveAPet
{
    public class Animal
    {

        private readonly LinkedList<Animal> _children;
        private readonly Animal[] _parent;
        private readonly Genome _genome;
        public string Name { set; get; }
        public Genome Genome { get { return _genome; } }
        public LinkedList<Animal> Children { get { return _children; } }
        public Animal[] Parent { get { return _parent; } }
        public Body Body { get; private set; }
        // each animal will be given a genome and its parent
        public Animal(Genome g, Animal[] parent)
        {

            _genome = g;
            _children = new LinkedList<Animal>();
            _parent = parent;
            Body = new Body();


        }

        public Animal(byte[] binary)
        {

            //deserialize the binary and load the animal

        }

        public void Hatch()
        {


            CreateBody();

        }

        public void CreateFamilyTree()
        {

            // might not be void
            // creates the family tree we want
            throw new NotImplementedException("Familty tree");

        }

        private void CreateBody()
        {
            Dictionary<BodyPartType, LinkedList<object>> allTraits = new Dictionary<BodyPartType, LinkedList<object>>();
            int size = Enum.GetNames(typeof(BodyPartType)).Length;
            for (int i = 0; i < size; i++)
            {
                allTraits.Add((BodyPartType)i, _genome.Decode((BodyPartType)i));

            }
            Body.Hatch(allTraits);


        }


        public void Mutate(string serializedGene, BodyPartType index, ChromosomePair cp)
        {
            Gene gene = new Gene(serializedGene);
            LinkedList<object> traits = _genome.Mutate(gene, index, cp);
            Body.Change(index, traits);


        }

        public Animal Breed(Animal mate)
        {
            // the breeding process
            throw new NotImplementedException("breeding");
        }

        public byte[] serialize()
        {
            throw new NotImplementedException("serialize animal");

        }


    }
}
