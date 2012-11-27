<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoServicio.FinanzasService
{
    public partial class Documento
    {
        public override string ToString()
        {
            return Fecha1.ToShortDateString() + " - " + Consecutivo + "  Total: " + Total.ToString();
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoServicio.FinanzasService
{
    public partial class Documento
    {
        public override string ToString()
        {
            return Fecha1.ToShortDateString() + " - " + Consecutivo + "  Total: " + Total.ToString();
        }
    }
}
>>>>>>> 674ab780c3ed7d7b8d2a0823c347c88227c2bea0
