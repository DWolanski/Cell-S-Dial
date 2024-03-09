function intersectRect(r1l, r1u, r1r, r1d, r2l, r2u, r2r, r2d)
{
    return !(r2l > r1r || r2r < r1l || r2u > r1d || r2d < r1u)
}